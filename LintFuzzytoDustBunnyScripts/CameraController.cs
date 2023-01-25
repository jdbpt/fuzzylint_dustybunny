using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ////look at player position and do not follow the y position. 
        //transform.LookAt(new Vector3(player.transform.position.x, -1f, player.transform.position.z));
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 5f);

        
    }
}
