using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb; //reference to the rigidboy component on this object

    [SerializeField]
    float jumpForce; //the VERTICAL force per-jump.

    [SerializeField]    
    float horizontalForce; //the HORIZONTAL force per-jump

    [SerializeField]
    float downWardForce; //the DOWNWARD force for if the player holds DOWN while falling.

    Vector3 SpawnPosition;

    bool leftKeyDown; //detects whether the player is holding down A.
    bool rightKeyDown; //detects whether the player is holding down D.
    bool downKeyDown; //detects whether the player is holding down S.

    public GameObject playerDeathParticles;
    public bool isDead = false;
    public bool facingRight = true;
    public bool bufferRight;
    public bool bufferLeft;

    [SerializeField]
    float timeBetweenBuffer = 0.2f;

    float timeSinceLeftBuffer = 0;
    float timeSinceRightBuffer = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //assign rigidbody reference variable.
        SpawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftKeyDown && bufferLeft == false)
        {
            bufferLeft = true;
            timeSinceLeftBuffer = 0;
        }

        if (rightKeyDown && bufferRight == false)
        {
            bufferRight = true;
            timeSinceRightBuffer = 0;
        }

        if (bufferRight == true)
        {
            timeSinceRightBuffer += Time.deltaTime;
        }

        if (bufferLeft == true)
        {
            timeSinceLeftBuffer += Time.deltaTime;
        }


        if (timeSinceLeftBuffer >= timeBetweenBuffer)
        {
            bufferLeft = false;
        }

        if (timeSinceRightBuffer >= timeBetweenBuffer)
        {
            bufferRight = false;    
        }
    }

    public void PerformHop()
    {
        GameManager.instance.WingFlapSFX();
        Mathf.Clamp(rb.velocity.x, -10, 10);
        Mathf.Clamp(rb.velocity.y, -10, 50);

        /*
        //if the player is SMASHING the down key, give them some downward force for game-feel purposes.
        if (downKeyDown)
        {
            rb.AddForce(-transform.up * downWardForce);
            Debug.Log("you are holding the down key");
        }
        */

        if (facingRight)
        {
            rb.AddForce(transform.right * horizontalForce);
            //Debug.Log("you are holding the right key.");
        }

        if (!facingRight)
        {
            rb.AddForce(-transform.right * horizontalForce);
            //Debug.Log("you are holding the left key");
        }
        //freeze rigidbody on the same frame that we apply force for jump consistency.
        rb.velocity = new Vector2(0, 0);

        //add force in the upwards direction. Use the force variable assigned in inspector.
        rb.AddForce(transform.up * jumpForce);
    }

    public void SetLeftKey(bool held)
    {
        leftKeyDown = held;

        if (held == true && GetComponent<SpriteRenderer>().flipX == false)
        {
            if (facingRight == true)
            {
                facingRight = false;
            }
            //flip ducky sprite
            GetComponent<Flippable>().Flip();
            //flip gun sprite if the player has the gun
            if(GetComponentInChildren<PlayerGun>() != null)
                GetComponentInChildren<PlayerGun>().gameObject.GetComponent<Flippable>().Flip();
        }
    }

    public void SetRightKey(bool held)
    {
        rightKeyDown = held;
        if (held == true && GetComponent<SpriteRenderer>().flipX == true)
        {
            if (facingRight != true)
            {
                facingRight = true;
            }
            //flip ducky sprite
            GetComponent<Flippable>().FlipBack();
            //flip gun sprite if the player has the gun
            if(GetComponentInChildren<PlayerGun>() != null)
                GetComponentInChildren<PlayerGun>().gameObject.GetComponent<Flippable>().FlipBack();
        }
    }

    public void SetDownKey(bool held)
    {
        downKeyDown = held;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Goal")
		{
            Debug.Log("Level Completed");
            GameManager.instance.AdvanceLevel();
            GameManager.instance.WinSFX();
            collision.gameObject.GetComponent<Goal>().ChangeColor();
		}

        if (collision.gameObject.tag == "Map")
        {
            GameManager.instance.LandingSFX();
        }
    }

    public void Recoil() //runs when player fires gun. pushes you in inverse direction of where you're shooting.
    {
        rb.velocity = Vector2.zero;
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            rb.AddForce(transform.right * 1500);
        }
        else
        {
            rb.AddForce(-transform.right * 1500);
        }

        rb.AddForce(transform.up * 3000);

    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameObject g = Instantiate(playerDeathParticles);
        g.transform.position = transform.position;
        isDead = true;
        
        
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        isDead = false;
        transform.position = SpawnPosition;
    }


}
