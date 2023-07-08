using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }

    public void FlipBack()
    {
        GetComponent<SpriteRenderer>().flipX = false;
    }
}
