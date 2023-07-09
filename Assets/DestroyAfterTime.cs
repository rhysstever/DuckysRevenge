using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public Color myColor;
    public Color clear = new Color(255,255,255,0);
    // Start is called before the first frame update
    float a = 255;

    private void Awake()
    {
        myColor = GetComponent<SpriteRenderer>().color;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.05f);
    }
}
