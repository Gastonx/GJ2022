using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BoxCollider attackTrigger;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackTrigger.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }
}
