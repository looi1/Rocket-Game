using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour   
{
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioData;
    int health = 3;
    float delay = 2f;
    bool won = false;
    bool isTransitioning = false;
    bool crashToggle = false;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("you're good");
                    break;
                case "Fuel":
                    Debug.Log("Topped up fuel !");
                    break;
                case "Finish":
                    Debug.Log("Congrats you won !");
                    WinEffect();

                    break;
                default:
                    if (!crashToggle)
                    {
                        CrashEffect();
                    }
                    break;





            }
        }
        else
        {
            return;
        }
    }

    void CrashEffect()
    {

        audioData.Stop();
        audioData.PlayOneShot(crashAudio);
        crashParticles.Play();
        if (!won)
        {

            

            health -= 1;
            Debug.Log("You blew up and you have " + health + " health left.");

            if (health == 0)
            {
                isTransitioning = true;
                GetComponent<Movement>().enabled = false;
                Invoke("ReloadLevel", delay);
            }
        }


        
        
    }

    void WinEffect()
    {

        isTransitioning = true;
        audioData.Stop();
        successParticles.Play();
        audioData.PlayOneShot(successAudio);
        won = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // using string pros: can go back to menu or other specific page if needed, use index: since it is integer so can increment and go to next level
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex); // using string pros: can go back to menu or other specific page if needed, use index: since it is integer so can increment and go to next level
    }

    void DebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKey(KeyCode.R))
        {
            ReloadLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            crashToggle = !crashToggle; //Toggle collision, will flip true to false false to true
        }
    }
}
