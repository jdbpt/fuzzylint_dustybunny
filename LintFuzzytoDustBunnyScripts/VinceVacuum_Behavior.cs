using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//created by Jocelyn Brown 5/19/2022
//code sources: Enemy_0_NavMesh.cs from CodeName: Custodian and S-SI Type5_EnemyStates
//Code Monkey "How to use Unity NavMesh Pathfinding! (Unity Tutorial)" video at: https://www.youtube.com/watch?v=atCOd4o7tG4&list=WL&index=16
//Dave/GameDevelopment "FULL 3D ENEMY AI in 6 MINUTES!|| Unity Tutorial" video at: https://www.youtube.com/watch?v=UjkSFoLxesw&list=WL&index=29
//source code for instantiating projectile from Wall Ball project, as explained by Henry Bawden UDGE, Shooter.cs script
//Unity API reference on rigid body: https://docs.unity3d.com/ScriptReference/Rigidbody.html

public class VinceVacuum_Behavior : MonoBehaviour
{
    //calls to player transform and rigidbody and gameObject
    private GameObject player;
    private Transform playerTransform;

    //lose screen call
    public GameObject Lose_Respawn_Screen;//lose screen call

    private NavMeshAgent NMAgent;

    //patrol
    public bool retraceSteps = false;//public facing bool, can set true if want enemy to retrace their steps
    private bool onceTraversed = false; //to check if can retrace steps

    /*Naviation via points/objects in space that are set in the Editor
        Currently prefab comes with 2 points*/
    //source: https://docs.unity3d.com/Manual/nav-AgentPatrol.html

    [SerializeField] public Transform[] points;
    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        NMAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(GameObject.FindGameObjectWithTag("Player"));
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Nav Agents dest is: " + NMAgent.destination);
        playerTransform = player.transform;

        if(player.layer == 7)
        {
            ChaseState();
        }
        else
        {
            if (!NMAgent.pathPending && NMAgent.remainingDistance < 0.5f)
                PatrolState();
        }
    }

    private void PatrolState()
    {
        //source: https://docs.unity3d.com/Manual/nav-AgentPatrol.html

        // Returns if no points have been set up
        if (points.Length == 0)
        {
            return;
        }

        if (!retraceSteps)
        {
            // Set the agent to go to the currently selected destination.
            NMAgent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // and per Unity, restart from beginning of array if needed.  
            destPoint = (destPoint + 1) % points.Length;
        }
        else if (retraceSteps)
        {
            //NMAgent.destination = points[destPoint].position;
            if ((destPoint < (points.Length - 1)) && !onceTraversed)
            {
                NMAgent.destination = points[destPoint].position;
                destPoint += 1;
            }
            else if ((destPoint == (points.Length - 1)) && !onceTraversed)
            {

                NMAgent.destination = points[destPoint].position;
                onceTraversed = true;
                destPoint -= 1;
            }
            else if (((destPoint < (points.Length - 1)) && destPoint > 0) && onceTraversed)
            {
                NMAgent.destination = points[destPoint].position;
                destPoint -= 1;

            }
            else if (destPoint == 0 && onceTraversed)
            {

                NMAgent.destination = points[destPoint].position;
                onceTraversed = false;
                destPoint += 1;
            }
        }

    }

    private void ChaseState()
    {
        //source: https://docs.unity3d.com/Manual/nav-AgentPatrol.html

        // Set the agent to go to the currently selected destination of the player.
        Debug.Log("Player's transform position is: " + playerTransform.position);
        NMAgent.destination = player.transform.position;
       
        NMAgent.speed = 10f;//should move faster to get player
    }

    private void ResetPlayer()
    {
        Debug.Log("Resetting Player");
        Lose_Respawn_Screen.GetComponent<LoseRespawnScreen>().ActivateLoseRespawnScreen();
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetPlayer();
        }
    }

}
