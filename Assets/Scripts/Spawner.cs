using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy = null;
    void Awake()
    {
        var enemy = GameObject.Instantiate(_enemy, gameObject.transform.position, Quaternion.identity);
    }

}
