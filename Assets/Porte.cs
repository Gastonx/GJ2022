using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
  public static Porte instance;
  public int Objectif;
  public int Objectif1;
  public GameObject porte;
  public GameObject porte2;
  public bool porte1faite = false;

  public AudioSource secret;
    // Start is called before the first frame update
    void Start()
    {
      instance = this;
    }

    // Update is called once per frame
    void Update()
    {
      if(Objectif==0 && !porte1faite){
        secret.Play();
        Objectif = Objectif1;
        porte1faite = true;
        Destroy(porte);

      }
      if(Objectif==0 && porte1faite){
        secret.Play();
        Destroy(porte2);
      }

    }
}
