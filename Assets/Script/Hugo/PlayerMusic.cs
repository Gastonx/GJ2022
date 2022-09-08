using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip DeusVult;
    public AudioClip Hurt;
    
    public void playHurt() 
    {
        source.clip = Hurt;
        source.Play();
    }
    public void playDeus()
    {
        source.clip=DeusVult;
        source.Play();
    }


}
