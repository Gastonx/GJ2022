using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public float sightAngle;

    public Vector3 destination;

    private GameObject playerRef;

    public bool isMelee;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float blletReload;

    private bool startShooting;

    
    void Start() {
      //  behaviorState = EnemyBehaviorState.PATROL;
        
        GameManager.Instance.gameState = GameState.INTRO;
        GameManager.Instance.videoCanvas.SetActive(true);
        GameManager.Instance.videoDisplay.SetActive(true);
        GameManager.Instance.videoPlayer.clip = GameManager.Instance.clips[1];
        GameManager.Instance.videoPlayer.targetTexture = GameManager.Instance.clipsTextures[1];
        GameManager.Instance.videoDisplay.GetComponent<RawImage>().texture = GameManager.Instance.clipsTextures[1];
        GameManager.Instance.videoPlayer.Play();
      
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        //RandomPosition();
    }

    
    void Update() {
        
        if(destination != Vector3.zero)
            agent.SetDestination(destination);
        
        
        
        switch (behaviorState) {
            case EnemyBehaviorState.PATROL:
                if (agent.remainingDistance < 1f || destination == Vector3.zero) 
                    RandomPosition();
                
                Vector3 playerToEnemy = playerRef.transform.position - transform.position;
                Vector3 normalVector = Vector2.Perpendicular(transform.position);

                float side = Vector3.Dot(playerToEnemy, normalVector);
                Debug.DrawLine(transform.position,transform.position + (transform.right * -1 * sightRange),Color.yellow);
                
                if (side > 0) {
                    Vector3 sightVector = transform.right * -1 * sightRange;
                    float angle = Vector3.Angle(playerToEnemy, sightVector);

                    if (angle <= sightAngle && Vector3.Distance(transform.position, playerRef.transform.position) < sightRange) {
                        Debug.Log("see player");
                        behaviorState = EnemyBehaviorState.CHASE;
                    }
                }
                break;
            
            case EnemyBehaviorState.CHASE:
                destination = playerRef.transform.position - ( playerRef.transform.position - transform.position).normalized * (isMelee ? 15 : 50);
                
                if (agent.remainingDistance < 1f) 
                    behaviorState = EnemyBehaviorState.ATTACK;
                
                break;
            
            case EnemyBehaviorState.ATTACK:

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
        Debug.Log("generate next destination");
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
        Debug.Log("launch bullet");

        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        Physics.IgnoreCollision(GetComponent<Collider>(),playerRef.GetComponent<Collider>());
        
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
            rb.velocity = (playerRef.transform.position - transform.position).normalized * bulletSpeed;
        }

        StartCoroutine(LaunchBullet());
    }

}
