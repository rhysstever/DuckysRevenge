using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    PlayerMovement playerMovement; //reference to the PlayerMovement script on my child object.
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //this script's purpose is to detect input. That's what we're doing!
        DetectInput();
    }

    void DetectInput()
    {
        //directional input detection. allow various keys to detect this for accessibility's sake.
        playerMovement.SetLeftKey(Input.GetKey("a") || Input.GetKey("left"));
        playerMovement.SetRightKey(Input.GetKey("d") || Input.GetKey("right"));
        //playerMovement.SetDownKey(Input.GetKey("s") || Input.GetKey("down"));

        //detect jump input. allow various keys to detect this for accessibility's sake.
        if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            playerMovement.PerformHop(); //tell the player sprite to do its jump.
        } 

        if (Input.GetKeyDown("r"))
        {
            //reload scene to test music.
            Scene cScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cScene.name.ToString());
        }
    }
}
