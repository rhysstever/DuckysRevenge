using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    // Set in inspector
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float fireRate, projectileSpeed;

    // Set at Start
    private float fireTimer, shotVariationAmount;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start firing at a random time
        fireTimer = Random.Range(0.0f, fireRate);

        shotVariationAmount = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate && GameManager.instance.player != null)
            {
                GameObject playerBody = GameManager.instance.player.transform.GetChild(0).GetChild(0).gameObject;

                //if(!HasLineOfSight(playerBodyPos))
                //    return;

                Fire(playerBody);

                // Reset timer
                fireTimer = 0.0f;
            }
        }
        
    }

	private void Fire(GameObject targetObject)
    {
        Vector3 startingPos = new Vector3(
            transform.position.x,
            transform.position.y + 0.25f,
            0.0f
        );

        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, startingPos, Quaternion.identity, GameManager.instance.projectilesParent.transform);
        projectile.GetComponent<Projectile>().source = gameObject;
        projectile.GetComponent<Projectile>().speed = projectileSpeed;

        // Get the target position
        Vector3 targetPos = targetObject.transform.position;
        targetPos = AddVariance(targetPos, shotVariationAmount);

        // Rotate the projectile to face the target
        projectile.transform.LookAt(targetPos);
        projectile.GetComponent<Projectile>().target = targetPos;

        GameManager.instance.BowShotSFX();
    }

    private Vector2 AddVariance(Vector2 vector2, float varianceAmount)
	{
        float varPosX = Random.Range(-varianceAmount, varianceAmount);
        float varPosY = Random.Range(-varianceAmount, varianceAmount);

        vector2.x += varPosX;
        vector2.y += varPosY;

        return vector2;
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        //play some kind of fun effect showing your kill!
    }
}
