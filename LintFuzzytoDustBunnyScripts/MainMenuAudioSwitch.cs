using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioSwitch : MonoBehaviour
{
    public GameObject LoreInsert;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LoreInsert.activeSelf)
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
