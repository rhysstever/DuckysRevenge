using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public GameObject archerDeathParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Archer")
        {
            Debug.Log("shot an enemy.");
            GameObject p = Instantiate(archerDeathParticles);
            p.transform.position = collision.transform.position;
            GameManager.instance.ArcherDeathSFX();

            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Destroy(gameObject, 1);
            Destroy(p, 5);
        }
       
        
        
    }
}
