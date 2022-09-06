using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoove3D : MonoBehaviour
{
  [SerializeField] public int HorizontalSpeed;
  [SerializeField] public int VerticalSpeed;
  [SerializeField] public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector3(Input.GetAxis("Horizontal") * HorizontalSpeed,0, -(Input.GetAxis("Vertical") * VerticalSpeed));
    }
}
