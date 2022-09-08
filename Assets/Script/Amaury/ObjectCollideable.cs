using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectCollideable : MonoBehaviour {

    public List<int> ignoreLayers;
    public UnityEvent destroyActions;

    public bool isDestroyable;
    
    private void Start() {
        ignoreLayers = new List<int>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!ignoreLayers.Contains(collision.gameObject.layer)) {
            destroyActions?.Invoke();
            
            if(isDestroyable)
                Destroy(transform.gameObject);
        }
    }
}
