using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchCradle_Handler : MonoBehaviour
{
    public GameObject congratsParticles;
    public GameObject WinScreen;
    public GameObject LoreInsert;
    public float timeTillWinScreenShows = 2f;

   
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            congratsParticles.SetActive(true);
            GetComponent<AudioSource>().Play();
            Invoke(nameof(CallWinScreen), timeTillWinScreenShows);//after two seconds set it active
        }
    }

    private void CallWinScreen()
    {
        WinScreen.SetActive(true);
        LoreInsert.SetActive(true);
    }


}
