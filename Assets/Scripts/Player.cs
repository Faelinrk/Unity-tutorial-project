using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage, IGetHealed, IGetCurrentHealth, IGetAmmo, IBoostSpeed
{
    //Пуля
    [SerializeField] private GameObject _bulletPref = null;
    [SerializeField] private Transform _bulletStartPosition = null;
    //Мина
    [SerializeField] private GameObject _landMine = null;
    [SerializeField] private Transform _landMinePosition = null;
    // Передвижение/Вращение
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _rotSpeed = 45;
    private Vector3 _direction = Vector3.zero;
    public bool canjump = true;
    [SerializeField] private float _jumppower = 20000;
    //Здоровье
    [SerializeField] private int _hp = 100;
    public bool isAlive = true;
    //Патроны и мины
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _minesCount = 1;
    //Стрельба/Расстановка мин
    bool fire = false;
    bool minespawn = false;
    private int _bulletDamage = 4;
    private int _landmineDamage = 5;
    private float _bulletPower = 300;
    private Animator _animator = null;
    private bool _doublejump = false;
    private float _timeholded = 0f;
    //Аудио
    private AudioSource _audioSource = null;
    [SerializeField] AudioClip _shotSound = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Выстрел
        if (Input.GetMouseButtonDown(0))
        {
            fire = true;
        }
        //Мина
        if (Input.GetMouseButtonDown(1))
        {
            minespawn = true;
        }

        _direction.z = Input.GetAxis("Vertical");
        CheckJump();
        if (Input.GetKey(KeyCode.Space))
        {
            _timeholded += Time.deltaTime;
            if (_timeholded > 0.5f)
            {
                _doublejump = true;
            }
        }
        else
            _timeholded = 0f; 

}

    void FixedUpdate()
    {
        // Инициализация стрельбы или мины
        if (fire && _ammoCount > 0)
            Fire();
        else if (_ammoCount <= 0)
            fire = false;
        if (minespawn && _minesCount > 0)
            LandMine();
        else if (_minesCount <= 0)
            minespawn = false;
        Move();
    }

    private void Move()
    {
        if (_direction != Vector3.zero)
        {
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
        // Движение
        var speed = _speed * _direction * Time.fixedDeltaTime;
        transform.Translate(speed);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * _rotSpeed * Time.fixedDeltaTime, 0));

    }

    private void CheckJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (canjump)
            {
                if (_doublejump)
                {
                    GetComponent<Rigidbody>().AddForce(transform.up * _jumppower * 2);
                    canjump = false;
                    _doublejump = false;
                }
                else
                {
                    GetComponent<Rigidbody>().AddForce(transform.up * _jumppower);
                    canjump = false;
                }
            }
        }

    }
    private void LandMine()
    {
        // Установка мины
        var landmine = GameObject.Instantiate(_landMine, _landMinePosition.position, _landMinePosition.rotation).GetComponent<LandMine>();
        landmine.Init(_landmineDamage);
        minespawn = false;
        _minesCount -= 1;
    }
    private void Fire()
    {
        // Стрельба
        var bullet = GameObject.Instantiate(_bulletPref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Bullet>();
        bullet.Init(_bulletDamage);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletPower);
        bullet.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 100);
        fire = false;
        _ammoCount -= 1;
        if (!GameSettings.instance.Mute)
            _audioSource.PlayOneShot(_shotSound,GameSettings.instance.Volume);
    }

    public void TakeDamage(int damage)
    {
        // Получение урона
        _hp -= damage;
        if (_hp <= 0)
            Death();
    }

    private void Death()
    {
        // Смерть
        isAlive = false;
        Destroy(gameObject);
    }


    public void GetHealed(int heal)
    {
        // Лечение
        if (_hp < 100)
        {
            _hp += heal;
            if (_hp > 100)
                _hp = 100;
        }
    }

    public int GetCurrentHealth()
    {
        // Узнать количество ХП персонажа
        return _hp;
    }

    public void GetAmmo(int ammo, int landmines)
    {
        // Получить патроны
        _ammoCount += ammo;
        _minesCount += landmines;
    }

    public void BoostSpeed(float boost)
    {
        _speed += boost;
    }
}
