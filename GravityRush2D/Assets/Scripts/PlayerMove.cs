using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerMovement;
    public float playerJumpSpeed;
    private bool gravFlipped;
    private float distToGround;


    // Start is called before the first frame update
    void Start()
    {
        gravFlipped = false;
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Gravity Switch
        if (Input.GetKeyDown(KeyCode.V))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -gameObject.GetComponent<Rigidbody2D>().gravityScale;
            gravFlipped = !gravFlipped;
        }

        //Player Movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * playerMovement * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * playerMovement * Time.deltaTime);
        }

        //Player Jumps
        if (Input.GetKeyDown(KeyCode.UpArrow) && !gravFlipped && isGrounded())
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && gravFlipped && isGrounded())
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, -Vector2.up, distToGround + .1f);
        if (hit.collider != null)
        {
            return true;
        }
        else return false;
    }

    
}