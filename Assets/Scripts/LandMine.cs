using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    private AudioSource _audioSource = null;
    [SerializeField] AudioClip _explodeSound = null;
    [SerializeField] int _boomForce = 2;
    [SerializeField] int _radius = 10;
    

    private int _damage = 0;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Init(int damage)
    {
        _damage = damage;
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (!GameSettings.instance.Mute)
                _audioSource.PlayOneShot(_explodeSound, (float)((GameSettings.instance.Volume) / 100));
            Explode(other);
        }


    }

    private void Explode(Collider other)
    {
        
        foreach (Collider collider in Physics.OverlapSphere(transform.position, _radius))
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
            {
                collider.GetComponent<ITakeDamage>()?.TakeDamage(_damage);
                collider.transform.Translate((collider.transform.position - transform.position) * _boomForce);
            }
        }

        Destroy(gameObject);

    }

}
