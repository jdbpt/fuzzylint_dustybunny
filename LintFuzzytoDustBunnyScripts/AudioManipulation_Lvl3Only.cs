using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManipulation_Lvl3Only : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject ThankYou;
    public GameObject level3WinMusic;
    public GameObject ThankYouMusic;
    public GameObject fanHolder;
    public GameObject vacuumHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WinScreen.activeSelf)
        {
            GetComponent<AudioSource>().enabled = false;
            level3WinMusic.SetActive(true);

            //to avoid extra noise
            fanHolder.SetActive(false);
            vacuumHolder.SetActive(false);
        }
        if (ThankYou.activeSelf)
        {
            level3WinMusic.SetActive(false);
            ThankYouMusic.SetActive(true);
        }
    }
}
