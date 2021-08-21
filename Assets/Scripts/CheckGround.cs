using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private GameObject owner = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            owner = GameObject.FindGameObjectWithTag("Player");
            owner.GetComponent<Player>().canjump = true;
        }
        
    }
}
