using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      this.GetComponent<Rigidbody2D>().velocity = new Vector2(5,0);
    }

    // Update is called once per frame
    void Update()
    {
      if(this.transform.localPosition.x>0.5f){
     Destroy(gameObject);
      }
    }
    private void OnTriggerEnter2D(Collider2D other){
      print("Ouch Dmg");
    }
}
