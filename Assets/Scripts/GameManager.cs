using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Code
    public static GameManager instance = null;

    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    // Set in inspector
    public GameObject archersParent, projectilesParent, player;
    public AudioClip gunShot, bowShot, arrowImpact, archerDeath, wingFlap, landing, playerDeath;
    public AudioSource audio;
    // Start is called before the first frame update

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GunshotSFX()
    {
        audio.PlayOneShot(gunShot);
    }

    public void BowShotSFX()
    {
        audio.PlayOneShot(bowShot, 0.1f);
    }

    public void ArrowImpactSFX()
    {
        audio.PlayOneShot(arrowImpact);
    }

    public void ArcherDeathSFX()
    {
        audio.PlayOneShot(archerDeath);
    }

    public void WingFlapSFX()
    {
        audio.PlayOneShot(wingFlap, 0.1f);
    }

    public void LandingSFX()
    {
        audio.PlayOneShot(landing, 0.025f);
    }

    public void PlayerDeathSFX()
    {
        audio.PlayOneShot(playerDeath);

    public void AdvanceLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
        //// Get the current scene's path
        //string currentScenePath = SceneManager.GetActiveScene().path;
        //int currentIndex = scenes.IndexOf(currentScenePath);

        if(currentIndex >= SceneManager.sceneCountInBuildSettings)
		{
            // There are no more scenes, the player has won the game
            Debug.Log("Game won!");
		} 
        else
		{
            SceneManager.LoadScene(currentIndex + 1);
		}

    }
}
