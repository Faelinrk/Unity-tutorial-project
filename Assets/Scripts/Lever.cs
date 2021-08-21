using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Door _door = null;
    private bool _usable = true;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _usable)
        {
            _door.GetComponent<Door>().isActive = true;
            _usable = false;
        }
    }

}
