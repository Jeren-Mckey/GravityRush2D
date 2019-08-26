﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float playerMovement;
    private bool gravFlipped;
    private float distToGround;
    private int playerJumps;
    private int gravSwitches;
    public GameObject playerObject;
    public GameObject spawnPoint;
    private float objectHeight;
    private Vector2 screenBounds;



    // Start is called before the first frame update
    void Start()
    {
        gravFlipped = false;
        playerJumps = 1;
        gravSwitches = 1;
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        GameManager.playerHitDelegate += spawnPlayer;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
            Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate()
    {
        //Player Camera Interaction
        if (isGrounded())
        {
            playerJumps = 1;
            gravSwitches = 1;
        }

        //Gravity Switch
        if (Input.GetKeyDown(KeyCode.V) && gravSwitches > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -gameObject.GetComponent<Rigidbody2D>().gravityScale;
            gravFlipped = !gravFlipped;
            if (gravFlipped) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
            else gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            gravSwitches--;
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && !gravFlipped && playerJumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            playerJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && gravFlipped && playerJumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -4), ForceMode2D.Impulse);
            playerJumps--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            playerJumps = 1;
            gravSwitches = 1;
        }
        else if (collision.collider.tag == "Enemy")
        {
            //Reset Gravity
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            
            //Trigger event
            GameManager.OnPlayerHit();
            GameManager.playerHitDelegate -= spawnPlayer;
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Obstacle")
        {
            //Reset Gravity
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

            //Particle Effects and Other things to add

            //Trigger event
            GameManager.OnPlayerHit();
            GameManager.playerHitDelegate -= spawnPlayer;
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Exit")
        {
            GameManager.CurrentLevel++;
            SceneManager.LoadScene("Level" + GameManager.CurrentLevel.ToString());
        }
    }

    void spawnPlayer()
    {
        GameObject go = Instantiate(playerObject, spawnPoint.transform.position, Quaternion.identity);
        go.name = gameObject.name;
        
    }

    bool isGrounded()
    {
        if ((-1 * (transform.position.y - objectHeight)) >= screenBounds.y) return true;
        else if ((-1 * (transform.position.y + objectHeight)) >= screenBounds.y) return true;
        else return false;
    }
}