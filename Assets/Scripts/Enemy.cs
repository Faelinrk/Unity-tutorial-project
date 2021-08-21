using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _hp = 100;
    public Transform[] ways = null;
    public bool isAlive = true;
    private NavMeshAgent navMeshAgent = null;
    private int _currentPoint = 0;
    public bool playerInVision = false;
    private Animator _animator = null;
    public GameObject player = null;
    [SerializeField] private ParticleSystem _blood = null;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        // Движение к первой точке
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(ways[0].position);
    }
    private void Update()
    {
        if (!playerInVision)
        {
            // Последующее движение
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                _currentPoint = (_currentPoint + 1) % ways.Length;
                navMeshAgent.SetDestination(ways[_currentPoint].position);
            }
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
        }

        if (navMeshAgent.hasPath)
        {
            _animator.SetBool("EnemyMoove", true);
        }
        else
        {
            _animator.SetBool("EnemyMoove", false);
        }
    }
    public void TakeDamage(int damage)
    {
        
        // Получение урона, смерть
        _hp -= damage;
        _blood.Play();
        if (_hp <= 0)
            Death();
    }

    private void Death()
    {
        //Смерть
        isAlive = false;
        Destroy(gameObject);
    }
}
