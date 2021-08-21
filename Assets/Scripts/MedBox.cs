using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBox : MonoBehaviour
{
    [SerializeField] private int _heal = 10;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<IGetCurrentHealth>().GetCurrentHealth()<100)
        {
            other.GetComponent<IGetHealed>().GetHealed(_heal);
            Destroy(gameObject);
        }
    }


}
