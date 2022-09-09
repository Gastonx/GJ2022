using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touch " + transform.parent.GetComponent<Animator>().GetBool("isAttacking"));
        if (other.tag== "Ennemi" && transform.parent.GetComponent<Animator>().GetBool("isAttacking"))
        {
            Debug.Log("touch");
            Destroy(other.gameObject);
        }
    }
}
