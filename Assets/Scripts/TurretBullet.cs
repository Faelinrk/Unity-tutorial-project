using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private int _damage = 0;
    [SerializeField] private float _speed = 15;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
        
    }
    public void Init(int damage)
    {
        _damage = damage;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ITakeDamage>().TakeDamage(_damage);
            Destroy(gameObject);
        }

    }
}