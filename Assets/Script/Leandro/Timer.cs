using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
  public static Timer instance;

  //public Text timer;
  public float temps;
  public Slider Vie;
    // Start is called before the first frame update
    void Start()
    {
      if (instance == null) {
        instance = this;
      }


    }

    // Update is called once per frame
    void Update()
    {
      temps-=Time.deltaTime;
      //timer.text = "" + (int)temps;

      if(temps<=0){
        //gameover
         SceneManager.LoadScene("GameOver");
      }

      Vie.value = temps;
    }

    public void TakeDamage(float damage){
      temps-=damage;
    }

    public void Heal(float heal){
      Porte.instance.Objectif -= 1;
      temps+=heal;
    }

    public void gameover(){
    //SceneManager.LoadScene("Scene a charger");
    }
}
