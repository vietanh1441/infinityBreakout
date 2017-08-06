using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    GameObject GM;

    //Make the min and max speed to be configurable in the editor.
    public float MinimumSpeed = 25;
    public float MaximumSpeed = 30;

    private float min_cache, max_cache;
    //To prevent the ball from keep bouncing horizontally we enforce a minimum vertical movement
    public float MinimumVerticalMovement = 0.3F;

    //Don't move the ball unless you tell it to
    private bool hasBeenLaunched = false;

    public GameObject Text;

    Rigidbody2D rigidBody;

    //Start is called one time when the scene has been loaded
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
        rigidBody = GetComponent<Rigidbody2D>();
        min_cache = MinimumSpeed;
        max_cache = MaximumSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Text.GetComponent<Text>().text = " " + transform.position.x + " " + transform.position.y + " " + transform.position.z;
        if (hasBeenLaunched)
        {


            //Get current speed and direction
            Vector3 direction = rigidBody.velocity;
            float speed = direction.magnitude;

            direction.Normalize();

            //Make sure the ball never goes straight horizotal else it could never come down to the paddle.
            if (direction.y > -MinimumVerticalMovement && direction.y < MinimumVerticalMovement)
            {
                //Adjust the y, make sure it keeps going into the direction it was going (up or down)
                direction.y = direction.y < 0 ? -MinimumVerticalMovement : MinimumVerticalMovement;

                //Adjust the x also as x + y = 1
                direction.x = direction.x < 0 ? -1 + MinimumVerticalMovement : 1 - MinimumVerticalMovement;

                //Apply it back to the ball
                rigidBody.velocity = direction * speed;
            }

            if (speed < MinimumSpeed || speed > MaximumSpeed)
            {
                //Limit the speed so it always above min en below max
                speed = Mathf.Clamp(speed, MinimumSpeed, MaximumSpeed);

                //Apply the limit
                //Note that we don't use * Time.deltaTime here since we set the velocity once, not every frame.
                rigidBody.velocity = direction * speed;
            }
            
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Launch();
            }
        }
    }

    public void SlowDown()
    {
        MinimumSpeed = MinimumSpeed / 5;
        MaximumSpeed = MaximumSpeed / 5;
    }

    public void NormalSpeed()
    {
        MinimumSpeed = min_cache;
        MaximumSpeed = max_cache;
    }

    public void Launch()
    {
        Vector3 launchDirection = new Vector3(0, -1, 0);
        launchDirection = launchDirection.normalized ;
        GetComponent<Rigidbody2D>().velocity = launchDirection;


        //Apply it to the rigidbody so it keeps moving into that direction (untill it hits a block or wall ofcourse)
        GetComponent<Rigidbody2D>().velocity = launchDirection;

        hasBeenLaunched = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       //x Debug.Log("Hit");
        if (collision.collider.tag == "Paddle")
        {
            Debug.Log("Hit");
            rigidBody.velocity = new Vector3(transform.position.x - collision.transform.position.x,
                                            transform.position.y - collision.transform.position.y + 1f,
                                            0)
                                            .normalized * MinimumSpeed;

            
            GM.SendMessage("EndTransition");
            /*rigidBody.AddForce(new Vector3(transform.position.x - collision.transform.position.x,
                                            transform.position.y - collision.transform.position.y)
                                            ,ForceMode.VelocityChange);*/
        }
    }

   

}
