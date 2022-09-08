using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoovementThirdPerson : MonoBehaviour
{


  public CharacterController controller;
  public Transform cam;
  public Transform point;
 
  public UnityEvent deusVult;
  public Transform perso;
  public float vitRota = 0.1f;

  public float speed = 6f;

  public float turnSmoothTime = 0.1f;
  public Quaternion sheesh = Quaternion.Euler(0f,0f,0f);

  public Animator animator;

  float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown("l") || Input.GetButton("L1"))
      {
        sheesh = Quaternion.Euler(sheesh.eulerAngles.x,sheesh.eulerAngles.y-vitRota,sheesh.eulerAngles.z);
      }
      if (Input.GetKeyDown("r") || Input.GetButton("R1")){
        sheesh = Quaternion.Euler(sheesh.eulerAngles.x,sheesh.eulerAngles.y+vitRota,sheesh.eulerAngles.z);
      }

        if (Input.GetButtonDown("AttackButton"))
        {
            Attack();
            
        }
        if (Input.GetButtonDown("ShieldButton"))
        {
            Shield();
        }

      if (Input.GetKey("o") || Input.GetButton("R1")&&Input.GetButton("L1")){
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
      
      animator.SetFloat("speed",direction.magnitude);
      
    }

    void Attack()
    {
        deusVult.Invoke();
        Debug.Log("Attack");
        animator.SetBool("isAttacking",true);
        StartCoroutine(WaitEndAnimation("isAttacking",1f));
    }

    void Shield()
    {
        Debug.Log("Defend");
        animator.SetBool("isProtecting",true);
        StartCoroutine(WaitEndAnimation("isProtecting",1.5f));
    }

    private IEnumerator WaitEndAnimation(string animParam,float seconds)
    {
      yield return new WaitForSeconds(seconds);
      
      animator.SetBool(animParam,false);
      Debug.Log("entered");
    }
}
