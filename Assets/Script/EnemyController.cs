using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyBehaviorState {
    PATROL,
    CHASE,
    ATTACK
}


public class EnemyController : MonoBehaviour  {
    public EnemyBehaviorState behaviorState;
    private NavMeshAgent agent;

    public Transform[] patrolPoints;
    public float sightRange;
    private Color sightColor;

    public Vector3 destination;
    
    void Start() {
        behaviorState = EnemyBehaviorState.PATROL;
        agent = GetComponent<NavMeshAgent>();
        
        //RandomPosition();
    }

    
    void Update() {
        switch (behaviorState) {
            case EnemyBehaviorState.PATROL:
                sightColor = Color.yellow;
                if (agent.remainingDistance < 1f && destination != Vector3.zero) 
                    agent.SetDestination(destination);
                else 
                    RandomPosition();
                break;
            
            case EnemyBehaviorState.CHASE:
                sightColor = Color.red;
                break;
        }

    }

    private void RandomPosition() {
        Debug.Log("generate next destination");
        int minX = Mathf.Min((int)patrolPoints[0].position.x,(int)patrolPoints[1].position.x);
        int maxX = Mathf.Max((int)patrolPoints[0].position.x, (int)patrolPoints[1].position.x);
         
        int minZ= Mathf.Min((int)patrolPoints[0].position.z,(int)patrolPoints[2].position.z);
        int maxZ = Mathf.Max((int)patrolPoints[0].position.z, (int)patrolPoints[2].position.z);

        int randX = Random.Range(minX, maxX);
        int randZ = Random.Range(minZ, maxZ);

      //  destination = new Vector3(randX, transform.position.y, randZ);
    }
    
    void OnDrawGizmos() {
        sightColor.a = 0.25f;
       // Gizmos.color = sightColor;
       // Gizmos.DrawSphere(this.transform.position, sightRange);
    }

}
