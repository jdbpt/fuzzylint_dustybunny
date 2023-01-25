using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//created by Jocelyn Brown, 5/18/2022

public class YellowPlatform_Master : MonoBehaviour
{
    private int count = 2;
    public GameObject LintEFX;
    public TextMeshProUGUI countText;

    private void Awake()
    {
        count = 2;
        LintEFX.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //countText.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= 0)
        {
            //turn off the appearance of the platform
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            LintEFX.SetActive(true);

            Invoke(nameof(countTextEmpty), 0.5f);//empty out text display in .5 second
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            count -= 1;//decrement count
            countText.text = count.ToString();
            GetComponent<AudioSource>().Play();
        }
    }


    private void countTextEmpty()
    {
        countText.text = " ";
    }
}
