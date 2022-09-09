using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
  public static Porte instance;
  public int Objectif;
  public GameObject porte;
    // Start is called before the first frame update
    void Start()
    {
      instance = this;
    }

    // Update is called once per frame
    void Update()
    {
      if(Objectif==0){
        Destroy(porte);
      }
    }
}
