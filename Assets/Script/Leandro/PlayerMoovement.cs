using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
  [SerializeField] public int HorizontalSpeed;
  [SerializeField] public int VerticalSpeed;
  [SerializeField] public Rigidbody2D rb;
  [SerializeField] public GameObject[] Prefabs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector2(Input.GetAxis("Horizontal") * HorizontalSpeed,Input.GetAxis("Vertical") * VerticalSpeed);
      if (Input.GetKeyDown("r")){
      var Hitbox = Instantiate(Prefabs[0], rb.transform.position, Quaternion.identity);
      Hitbox.transform.parent = rb.transform;
      Hitbox.transform.position = new Vector2(this.transform.position.x-0.5f,this.transform.position.y+1);
    }
    }
}
