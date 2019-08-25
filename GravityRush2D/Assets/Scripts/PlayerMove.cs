using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerMovement;
    public float playerJumpSpeed;
    private bool gravFlipped;
    private Vector3 velocity;
    private Vector2 input;
    private float distToGround;


    // Start is called before the first frame update
    void Start()
    {
        playerJumpSpeed = 5;
        playerMovement = 4;
        gravFlipped = false;
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        playerRun();
        playerJump();
        if (Input.GetButtonDown("F"))
        { 
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -gameObject.GetComponent<Rigidbody2D>().gravityScale;
            gravFlipped = !gravFlipped;
        }
    move(velocity, Time.deltaTime);
    }


    bool isGrounded()
    {
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, -Vector2.up, distToGround + .1f);
        if (hit.collider != null)
        {
            return true;
        }
        else return false;
    }

    void getInput()
    {
        //Get the axis input for our character.
        float horizontalAxis = Input.GetAxisRaw("Horizontal") * playerMovement;
        float verticalAxis = Input.GetAxisRaw("Vertical") * playerMovement;

        // Just store the input to be used elsewhere.
        input = new Vector2(horizontalAxis, verticalAxis);
    }

    void playerJump()
    {
        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !gravFlipped)
            {
                velocity.y = playerJumpSpeed;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && gravFlipped)
            {
                velocity.y = -playerJumpSpeed;
            }
            else velocity.y = 0;
        }
        
    }

    void playerRun()
    {
        //If our character is grounded then we can execute the following code.
        if (isGrounded())
        {
            // Now, run at that speed (with just a little bit of smoothing).
            float targetVx = playerMovement * input.x;
            velocity.x = Mathf.MoveTowards(velocity.x, targetVx, 10 * Time.deltaTime);
        }
    }

    void move(Vector3 velocity, float t)
    {
        transform.Translate(velocity * t);
    }
}