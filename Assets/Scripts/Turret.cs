using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _speed = 10;
    [SerializeField] private int _damage = 13;
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private float _dist = 20f;
    [SerializeField] private float _reloadTime = 1f;

    private bool _reload = false;
    

    private bool inDist = false;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        CheckAgro();
    }

    void CheckAgro()
    {
        //Поворот турели и стрельба
        var dir = _target.position - transform.position;
        var newDir = Vector3.RotateTowards(transform.forward, dir, _speed * Time.fixedDeltaTime, 0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        newDir.y = 0;


        if (inDist)
        {
            //Рэйкасты
            RaycastHit hit;
            var rayDir = _target.position - _bulletStartPosition.position ;
            rayDir.y = 0;
            Physics.Raycast(_bulletStartPosition.position, rayDir, out hit, Mathf.Infinity);
            Debug.DrawRay(_bulletStartPosition.position, rayDir);
            //Поворот
            transform.rotation = Quaternion.LookRotation(newDir);
            //Задержка стрельбы/Стрельба
            if (!_reload && (hit.collider.gameObject.CompareTag("Player")) )
            {
                TurretFire(_damage);
                _reload = true;
                Invoke("Reload", _reloadTime);
            }
                
        }
            
        if (Vector3.Distance(_target.position, transform.position) <= _dist)
        {
            inDist = true;
        }
        else if (Vector3.Distance(_target.position, transform.position) > _dist)
        {
            inDist = false;
        }
    }


    void TurretFire(int damage)
    {
        var bullet = GameObject.Instantiate(_bulletPref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<TurretBullet>();
        bullet.Init(_damage);
    }

    private void Reload()
    {
        _reload = false;
    }
}
