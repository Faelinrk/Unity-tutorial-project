using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class FloorButton : MonoBehaviour
{
    [SerializeField] Door _door = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
            _door.GetComponent<Door>().isActive = true;
    }
}
