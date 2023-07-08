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
        if(fireTimer >= fireRate && GameManager.instance.player != null)
		{
            GameObject playerBody = GameManager.instance.player.transform.GetChild(0).GetChild(0).gameObject;

            //if(!HasLineOfSight(playerBodyPos))
            //    return;

            Fire(playerBody);

            // Reset timer
            fireTimer = 0.0f;
        }
    }

	private void FixedUpdate()
	{
		fireTimer += Time.deltaTime;
	}

	//private bool HasLineOfSight(Vector3 targetPos)
	//{
	//	Vector3 direction = targetPos - transform.position;
	//	RaycastHit hit;
	//	int layerMask = 5;
	//	layerMask = ~layerMask;
	//	if(Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
	//	{
	//		return (hit.transform.gameObject.tag == "Player");
	//	}
	//	else
	//	{
	//		Debug.Log("Raycast missed");
	//		return false;
	//	}
	//}

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

        // Get the target position
        Vector3 targetPos = targetObject.transform.position;

        // Rotate the projectile to face the target
        projectile.transform.LookAt(targetPos);

        // Shoot the projectile away from the archer and towards the target
        Vector2 direction = new Vector2(
            targetPos.x - projectile.transform.position.x,
            targetPos.y - projectile.transform.position.y + 5.0f    // Add 5 to account for gravity
        );

        // Add variation to the shot's direction
        direction = AddVariance(direction, shotVariationAmount);
        direction *= projectileSpeed;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction);
    }

    private void Fire2(GameObject targetObject)
	{
        Vector3 startingPos = new Vector3(
            transform.position.x,
            transform.position.y + 0.25f,
            0.0f
        );

        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, startingPos, Quaternion.identity, GameManager.instance.projectilesParent.transform);
        projectile.GetComponent<Projectile>().source = gameObject;

        // Get the target position
        Vector3 targetPos = targetObject.transform.position;

        // Rotate the projectile to face the target
        projectile.transform.LookAt(targetPos);

        // Get angles to fire
        float? highAngle = 0f;
        float? lowAngle = 0f;
        CalculateAngleToHitTarget(projectile, targetObject, projectileSpeed, out highAngle, out lowAngle);


    }

    void CalculateAngleToHitTarget(GameObject projectile, GameObject target, float speed, out float? theta1, out float? theta2)
    {
        //Initial speed
        float v = speed;

        Vector3 targetVec = target.transform.position - projectile.transform.position;

        //Vertical distance
        float y = targetVec.y;

        //Reset y so we can get the horizontal distance x
        targetVec.y = 0f;

        //Horizontal distance
        float x = targetVec.magnitude;

        //Gravity
        float g = 9.81f;


        //Calculate the angles

        float vSqr = v * v;

        float underTheRoot = (vSqr * vSqr) - g * (g * x * x + 2 * y * vSqr);

        //Check if we are within range
        if(underTheRoot >= 0f)
        {
            float rightSide = Mathf.Sqrt(underTheRoot);

            float top1 = vSqr + rightSide;
            float top2 = vSqr - rightSide;

            float bottom = g * x;

            theta1 = Mathf.Atan2(top1, bottom) * Mathf.Rad2Deg;
            theta2 = Mathf.Atan2(top2, bottom) * Mathf.Rad2Deg;
        }
        else
        {
            theta1 = null;
            theta2 = null;
        }
    }

    private Vector2 AddVariance(Vector2 vector2, float varianceAmount)
	{
        float varPosX = Random.Range(-varianceAmount, varianceAmount);
        float varPosY = Random.Range(-varianceAmount, varianceAmount);

        vector2.x += varPosX;
        vector2.y += varPosY;

        return vector2;
    }
}
