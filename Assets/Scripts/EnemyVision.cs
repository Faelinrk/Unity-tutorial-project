using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    // Агр
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<Enemy>().player = other.gameObject;
            GetComponentInParent<Enemy>().playerInVision = true;

        }
    }

    // Потеря из вида
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("LoosePlayer", 3);
        }
    }

    private void LoosePlayer()
    {
        GetComponentInParent<Enemy>().player = null;
        GetComponentInParent<Enemy>().playerInVision = false;
    }

}
