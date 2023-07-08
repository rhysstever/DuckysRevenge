using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    //allows an object to move smoothly in a set direction.

    [SerializeField]
    [Header("Object Settings")]
    float speed = 10;
    [SerializeField]
    bool reversedDirection = false;

    Vector3 startingPosition; //samples obj starting position for reset
   

    // Start is called before the first frame update
    void Start()
    {
        if (reversedDirection)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        startingPosition = transform.position;

        SetSpeed(Random.Range(20,50));
    }

    // Update is called once per frame
    void Update()
    {
        //move the object in a certain direction.
        Vector3 move = new Vector3(0, 0, speed);
        transform.Translate(move * Time.deltaTime);


        //check for car offscreen.
        if (transform.position.x <= -30 || transform.position.x > 30)
        {
            //reset the car's position if it is off-screen.
            transform.position = startingPosition;
            SetSpeed(Random.Range(20, 40));
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
    }
}
