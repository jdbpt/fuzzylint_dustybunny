using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrannyFan_Behavior : MonoBehaviour
{

    //calls to player transform and rigidbody and gameObject
    private GameObject player;

    //lose screen call
    public GameObject Lose_Respawn_Screen;//lose screen call

    //set forces active on player when constant force enabled, depended on direction of fan
    public float forceX = 0;
    public float forceY = 0;
    public float forceZ = 0;

    //call to particle effect of wind
    public GameObject windParticle;

    public float secondsTillOff = 10f;//num of seconds if using on/off that coroutine will play

    public bool altOn_Off = false;

    private bool windActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (altOn_Off)
        {
            InvokeRepeating("WindControl", 0f, 10f);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetPlayer();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && windActive)//wind effect
        {
            other.gameObject.GetComponent<ConstantForce>().force = new Vector3(forceX, forceY, forceZ);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
        }
    }

    private void ResetPlayer()
    {
        Debug.Log("Resetting Player");
        Lose_Respawn_Screen.GetComponent<LoseRespawnScreen>().ActivateLoseRespawnScreen();

    }

    public IEnumerator windOffOn()
    {
        windParticle.SetActive(false);
        windActive = false;

        GetComponent<AudioSource>().Stop();

        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(secondsTillOff);
        windParticle.SetActive(true);
        windActive = true;
        GetComponent<CapsuleCollider>().enabled = true;

        GetComponent<AudioSource>().Play();
    }

    public void WindControl()
    {
        StartCoroutine(windOffOn());
    }
}
