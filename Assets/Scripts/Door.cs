using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    public bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            StartCoroutine(OpenDoor());
            isActive = false;
        }
    }

    IEnumerator OpenDoor()
    {
        while (transform.eulerAngles.y < 90)
        {
            transform.Rotate(new Vector3(0,_speed*Time.deltaTime,0));
            yield return null;
        } 
    }

}
