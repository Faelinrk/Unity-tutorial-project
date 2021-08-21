using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int _ammo = 10;
    [SerializeField] private int _mines = 10;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            other.GetComponent<IGetAmmo>().GetAmmo(_ammo,_mines);
            Destroy(gameObject);
        }
    }

}
