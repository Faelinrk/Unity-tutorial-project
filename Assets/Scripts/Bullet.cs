using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage = 0;
    [SerializeField] private float _speed = 15;


    public void Init(int damage)
    {
        _damage = damage;
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider; 
        // Столкновение с противником
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ITakeDamage>().TakeDamage(_damage);
            Destroy(gameObject);
        }

    }
}
