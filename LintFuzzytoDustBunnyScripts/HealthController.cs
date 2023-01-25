using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    public int playerHealth;
    [HideInInspector] public Vector3 playerStartPosition;//used to help reset the scene start conditions

    private const int MAXHEALTH = 3;
    private const int MINHEALTH = 0;

    public GameObject Lose_Respawn_Screen;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = MAXHEALTH;

        playerStartPosition = gameObject.transform.position;//getting start position of the player
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Vacuum"))
        //{
        //    HealthHandling();
        //}

        if (collision.gameObject.CompareTag("Fan"))
        {
            HealthHandling();
        }

        if (collision.gameObject.layer == 7)//what is carpet layer
        {
            HealthHandling();
        }
    }


    private void HealthHandling()
    {
            
         Invoke(nameof(LoseScreenCall), 2f);//delay call to allow vaccum to chase player
        
    }

    private void LoseScreenCall()
    {
        Lose_Respawn_Screen.GetComponent<LoseRespawnScreen>().ActivateLoseRespawnScreen();
    }

}
