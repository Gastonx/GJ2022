using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touch");
        if (other.tag== "Ennemi")
        {
            Debug.Log("touch");
            Destroy(other.gameObject);
        }
    }
}
