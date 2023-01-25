using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//created by Jocelyn Brown 5/19/2022 based on my script from S-SI Spr 2022, RespawnUI_Screen.cs
public class LoseRespawnScreen : MonoBehaviour
{
    public GameObject RespawnScreen;
    public GameObject loseScreen;
    
    private float timeTillRespawn = 20.0f;//initialize time to auto respawn 7 sec, sets the onscreen text and the coroutine to respawn

    private GameObject player;

    private int health;

    //public GameObject GreenObjsHolder;
    //public GameObject YellowObjsHolder;
    //public GameObject RedObjsHolder;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLoseRespawnScreen()
    {
        //make sure respawn screen is on and game is paused
        RespawnScreen.SetActive(true);

        loseScreen.SetActive(true);

        StartCoroutine(DeactivateRespawnScreen());

        


    }

    public IEnumerator DeactivateRespawnScreen()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(timeTillRespawn);

        //stop showing the respawn screen, and load in game again (automatic yes)
        RespawnScreen.SetActive(false);

        YesToRestartLevel();//active after the wait for seconds to restart
    }

    public void YesToRestartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);//reload the current scene
    }

}
