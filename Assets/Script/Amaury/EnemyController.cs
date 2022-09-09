using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EnemyBehaviorState {
    PATROL,
    CHASE,
    ATTACK
}


public class EnemyController : MonoBehaviour  {
    
    
    public static EnemyController Instance { get; private set;  }
    
    public EnemyBehaviorState behaviorState;
    private NavMeshAgent agent;

    public Transform[] patrolPoints;
    public float sightRange;
    public float sightAngle;

    public Vector3 destination;

    private GameObject playerRef;

    public bool isMelee;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float blletReload;

    private bool startShooting;

    public float attackDamage = 2.5f;
    public float heal;

    private void Awake()
    {
        Instance = this;
    }

    void Start() {
      //  behaviorState = EnemyBehaviorState.PATROL;



        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        //RandomPosition();
    }


    void Update() {
        if (destination != Vector3.zero) {
            agent.SetDestination(destination);
            transform.LookAt(destination);
        }

        switch (behaviorState) {
            case EnemyBehaviorState.PATROL:
              //  if (agent.remainingDistance < 1f || destination == Vector3.zero)
                   // RandomPosition();

                if (IsInSight())
                    behaviorState = EnemyBehaviorState.ATTACK;
                break;

            case EnemyBehaviorState.CHASE:
                //transform.LookAt(playerRef.transform);
                destination = playerRef.transform.position - ( playerRef.transform.GetChild(0).transform.position - transform.position).normalized *  18;

                if (agent.remainingDistance < 1f)
                    behaviorState = EnemyBehaviorState.ATTACK;

                break;

            case EnemyBehaviorState.ATTACK:

                
                Debug.Log("value " + IsInSight());
                if (!IsInSight())
                {
                    behaviorState = EnemyBehaviorState.PATROL;
                }

                // transform.LookAt(playerRef.transform);
               destination = Vector3.zero;
               transform.LookAt(playerRef.transform.GetChild(0).GetChild(1).position);
                if (!isMelee && !startShooting)
                {
                    StartCoroutine(LaunchBullet());
                    startShooting = true;
                }

                Debug.DrawLine(transform.position,transform.position + (playerRef.transform.position - transform.position).normalized * bulletSpeed,Color.red);
                break;
        }

    }

    private void RandomPosition() {

        Debug.Log("length: " + patrolPoints.Length);
        if (patrolPoints != null && patrolPoints.Length == 0)
        {
            Debug.Log("enter");
            return;
        }

        int minX = Mathf.Min((int)patrolPoints[0].position.x,(int)patrolPoints[1].position.x);
        int maxX = Mathf.Max((int)patrolPoints[0].position.x, (int)patrolPoints[1].position.x);

        int minZ = Mathf.Min((int)patrolPoints[0].position.z,(int)patrolPoints[2].position.z);
        int maxZ = Mathf.Max((int)patrolPoints[0].position.z, (int)patrolPoints[2].position.z);

        int randX = Random.Range(minX, maxX);
        int randZ = Random.Range(minZ, maxZ);

        destination = new Vector3(randX, transform.position.y, randZ);
    }

    private IEnumerator LaunchBullet() {
        yield return new WaitForSeconds(blletReload);
      //  Debug.Log("launch bullet");

        GameObject bullet = Instantiate(bulletPrefab,transform.GetChild(3).position,Quaternion.identity);

        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
            rb.velocity = (playerRef.transform.GetChild(0).position - transform.position).normalized * bulletSpeed;
        }

        if (behaviorState == EnemyBehaviorState.PATROL)
            yield break;

        StartCoroutine(LaunchBullet());
    }

    private bool IsInSight() {
      /*  Vector3 playerToEnemy =/* playerRef.transform.GetChild(0).position -  transform.position - playerRef.transform.GetChild(0).position;
        Vector3 normalVector = Vector2.Perpendicular(transform.position);
        
       
        float side = Vector3.Dot(playerToEnemy, normalVector);
        Debug.DrawLine(transform.position,transform.position + (transform.right * -1 * sightRange),Color.yellow);

        if (side > 0) {
            Debug.Log("side");
            Vector3 sightVector = transform.right * -1 * sightRange;
            float angle = Vector3.Angle(playerToEnemy, sightVector);

            if (angle <= sightAngle && Vector3.Distance(transform.position, playerRef.transform.GetChild(0).position) < sightRange) {
                Debug.Log("JE TE VOIS");
                return true;
            }
        }
*/
        return Vector3.Distance(playerRef.transform.GetChild(0).position,transform.position) <= sightRange;
    }

    private void OnDestroy() {
        Timer.instance.Heal(heal);
    }

   
}
