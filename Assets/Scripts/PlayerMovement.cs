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


    bool leftKeyDown; //detects whether the player is holding down A.
    bool rightKeyDown; //detects whether the player is holding down D.
    bool downKeyDown; //detects whether the player is holding down S.

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
        Mathf.Clamp(rb.velocity.x, -10, 10);
        Mathf.Clamp(rb.velocity.y, -50, 50);

        //if the player is SMASHING the down key, give them some downward force for game-feel purposes.
        if (downKeyDown)
        {
            rb.AddForce(-transform.up * downWardForce);
            Debug.Log("you are holding the down key");
        }

        if (bufferRight)
        {
            rb.AddForce(transform.right * horizontalForce);
            Debug.Log("you are holding the right key.");
        }

        if (bufferLeft)
        {
            rb.AddForce(-transform.right * horizontalForce);
            Debug.Log("you are holding the left key");
        }
        //freeze rigidbody on the same frame that we apply force for jump consistency.
        rb.velocity = new Vector2(0, 0);

        //add force in the upwards direction. Use the force variable assigned in inspector.
        rb.AddForce(transform.up * jumpForce);
    }

    public void SetLeftKey(bool held)
    {
        leftKeyDown = held;
    }

    public void SetRightKey(bool held)
    {
        rightKeyDown = held;
    }

    public void SetDownKey(bool held)
    {
        downKeyDown = held;
    }
}
