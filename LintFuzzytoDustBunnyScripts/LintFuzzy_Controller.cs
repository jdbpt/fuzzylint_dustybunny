using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//created by Jocelyn 5/17/2022
//reference on how to use GetButtondown and add a specific movement direction pos/neg: https://answers.unity.com/questions/145105/negative-button-binding.html
//code source for own gravity: https://answers.unity.com/questions/24240/changing-gravity-for-one-object.html,
//https://answers.unity.com/questions/26466/how-to-add-gravity-to-this-script.html#:~:text=By%20default%20its%20%22use%20gravity%22%20setting%20will%20be,of%20translating%20your%20object%2C%20you%20can%20apply%20forces.
public class LintFuzzy_Controller : MonoBehaviour
{
    public float playerSpeed = 10f;//speed player moves in X and Z
    public float bounceForce = 500f; //amount of force applied to make player bounce when hit whatIsGround layer
    
    public LayerMask whatIsGround;
    private Rigidbody playerRB;

    private bool isGrounded;//check to see if player grounded

    private bool doubleJump = false;//helps with cooldown with button press to make double jump

    private bool isDisabled = false;

    [SerializeField] private float playerGravity = -8.0f;//gravity on player

    public GameObject BounceReadyUI;//meter to let know when is grounded and able to jump
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        

        //set the cursor no visible, and locked to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        BouncingFuzzy();
        IsGroundedCheck();
        playerRB.angularVelocity = Vector3.zero;//zero out angular velocity to prevent rolling when hit edges
        playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);//set x and z velocity to zero but keep upward velocity
    }

    private void FixedUpdate()
    {
        //own gravity vs Unity gravity
        playerRB.AddForce(new Vector3(0, playerGravity, 0), ForceMode.Acceleration);//acceleration Force mode not take into account mass

        if (!isDisabled)
        {
            FuzzyControls();
        }
        

    }

    public void FuzzyControls()
    {

        if (Input.GetButton("Fwd/Bwd") && Input.GetAxisRaw("Vertical") > 0)
        {
            //playerRB.AddForce(Vector3.forward * playerSpeed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + playerSpeed * Time.deltaTime);
        }
        if (Input.GetButton("Fwd/Bwd") && Input.GetAxisRaw("Vertical") < 0)
        {
            //playerRB.AddForce(Vector3.forward * -playerSpeed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + -playerSpeed * Time.deltaTime);

        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            //playerRB.AddForce(Vector3.left * -playerSpeed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + playerSpeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            //playerRB.AddForce(Vector3.left * playerSpeed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + -playerSpeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);

        }
        if (Input.GetButtonDown("Fire1"))
        {

            if (isGrounded)
                Debug.Log("Pressed");
            {
                if (!doubleJump)
                {
                    StartCoroutine(nameof(extraBounce));
                }
            }

        }
    }
    public void IsGroundedCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, whatIsGround))
        {
            isGrounded = true;
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.blue);
            if (!doubleJump)
            {
                BounceReadyUI.SetActive(true);
            }
            
        }
        else
        {
            isGrounded = false;
            BounceReadyUI.SetActive(false);
        }
    }
    public void BouncingFuzzy()//this function is just for reference on where the ray cast is pointing in game window
    {

        //source: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        //source for is grounded: https://forum.unity.com/threads/detect-jump-only-if-isgrounded-true.472144/  (ArachnidAnimal)
        RaycastHit hit;

        var bounce = Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, whatIsGround);

        

        if(bounce)
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
           
            //Debug.Log("I'm hit");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)//6 is the whatIsGround layer
        {
            playerRB.AddForce(Vector3.up * bounceForce);//creates bouncing behavior when this is collided with
            GetComponent<AudioSource>().Play();
            //isGrounded = true;
        }
        if(collision.gameObject.layer == 7)//is carpet layer
        {
            isDisabled = true;//player controls will not work
            gameObject.layer = 7;//change layer to 7 so the vacuum will chase
        }
        if(collision.gameObject.layer == 8)//is couch layer
        {
            isDisabled = true;//player controls will not work
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            //isGrounded = false;
        }
    }

    IEnumerator extraBounce()
    {
        bounceForce *= 2;
        doubleJump = true;
        yield return new WaitForSeconds(1f);//wait a second then bounce force back down and doublejump is false to allow another jump
        bounceForce /= 2;
        doubleJump = false;
    }
}
