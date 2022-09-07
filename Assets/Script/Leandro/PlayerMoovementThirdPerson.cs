using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovementThirdPerson : MonoBehaviour
{
  public CharacterController controller;
  public Transform cam;
  public Transform point;
  public Transform perso;

  public float speed = 6f;

  public float turnSmoothTime = 0.1f;
  public Quaternion sheesh = Quaternion.Euler(0f,0f,0f);

  float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey("l")){
        sheesh = Quaternion.Euler(sheesh.eulerAngles.x,sheesh.eulerAngles.y-0.3f,sheesh.eulerAngles.z);
      }
      if (Input.GetKey("r")){
        sheesh = Quaternion.Euler(sheesh.eulerAngles.x,sheesh.eulerAngles.y+0.3f,sheesh.eulerAngles.z);
      }
      if (Input.GetKey("o")){
        sheesh = Quaternion.Euler(perso.rotation.eulerAngles.x,perso.rotation.eulerAngles.y,perso.rotation.eulerAngles.z);
      }
      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");
      Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

      if (direction.magnitude >= 0.1f){

        float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg+cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation= Quaternion.Euler(0f,angle,0f);
        Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
        controller.Move(moveDir.normalized*speed*Time.deltaTime);
      }
      point.rotation= sheesh;
    }
}
