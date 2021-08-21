using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject _owner = null;
    void Update()
    {
        transform.position =  new Vector3(_owner.transform.position.x, 3.14f, _owner.transform.position.z);
        transform.rotation = _owner.transform.rotation;
    }
}
