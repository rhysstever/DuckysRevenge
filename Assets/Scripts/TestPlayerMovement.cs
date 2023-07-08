using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = transform.position;

        if(Input.GetKey(KeyCode.W))
		{
            newPos.y += 0.1f;
		}

        if(Input.GetKey(KeyCode.S))
        {
            newPos.y -= 0.1f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            newPos.x -= 0.1f;
        }

        if(Input.GetKey(KeyCode.D))
        {
            newPos.x += 0.1f;
        }

        transform.position = newPos;
    }
}
