using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSon : MonoBehaviour
{
  public AudioSource source;
  public AudioClip ding;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
      source.clip = ding;
      source.Play();
    }
}
