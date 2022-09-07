using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviorState {
    PATROL,
    CHASE,
    ATTACK
}


public class EnemyController : MonoBehaviour
{

    public EnemyBehaviorState behaviorState;
    void Start() {
        behaviorState = EnemyBehaviorState.PATROL;
    }

    
    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 rand = Random.insideUnitCircle * 20;
            Vector3 randPos = new Vector3(rand.x, 0, rand.y);
            transform.position += randPos;
        }
        
        
        switch (behaviorState) {
            case EnemyBehaviorState.PATROL:
                OnPatrolState();
                break;
            
        }
        
    }

    private void OnPatrolState() {
        
    }

}
