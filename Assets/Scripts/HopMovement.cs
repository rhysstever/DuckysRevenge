using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopMovement : MonoBehaviour
{
    public float leapTime;
    public bool hopActive = false;
    public float jumpTimer = 0;
    public float jumpDistance = 0.5f;
    public float jumpHeight = 0.5f;

    Vector3 newPos;
    Vector3 startPos;
    Vector3 facingDirection;
    bool bufferPressed = false;
    float timeBetweenBufferResets = .3f;
    float timeSinceLastPress = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hopActive)
        {
            
            transform.position = Vector3.Lerp(startPos, newPos, jumpTimer / leapTime);

            jumpTimer += Time.deltaTime;

            if (transform.position == newPos)
            {
                hopActive = false;
                jumpTimer = 0;
            }
        }

        if (bufferPressed == true)
        {
            timeSinceLastPress += Time.deltaTime;

            if (timeSinceLastPress >= timeBetweenBufferResets)
            {
                bufferPressed = false;
            }
        }
    }

    public void Hop()
    {
        bufferPressed = true;
        timeSinceLastPress = 0;

        if (hopActive == false && bufferPressed == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);
            startPos = transform.position;
            newPos = new Vector3(transform.position.x, transform.position.y - jumpHeight, transform.position.z + jumpDistance);
            hopActive = true;
        }
        
        
    }

    public void SetRotation()
    {
        //set character y rotation based off of input
    }

}
