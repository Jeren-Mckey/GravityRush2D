using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerMovement;
    private bool gravFlipped;
    private float distToGround;
    private int playerJumps;
    private int gravSwitches;
    public GameObject playerObject;
    public GameObject spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        gravFlipped = false;
        playerJumps = 1;
        gravSwitches = 1;
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        LevelManager.playerHitDelegate += spawnPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Gravity Switch
        if (Input.GetKeyDown(KeyCode.V) && gravSwitches > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -gameObject.GetComponent<Rigidbody2D>().gravityScale;
            gravFlipped = !gravFlipped;
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
            LevelManager.OnPlayerHit();
            LevelManager.playerHitDelegate -= spawnPlayer;
            Destroy(gameObject);
        }
    }

    void spawnPlayer()
    {
        GameObject go = Instantiate(playerObject, spawnPoint.transform.position, Quaternion.identity);
        go.name = gameObject.name;
        
    }
}