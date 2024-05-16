using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour   
{
    int health = 3;
    float delay = 2f;
    bool won = false;
    void OnCollisionEnter(Collision collision)
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
                if (!won) { 
                    health -= 1;
                    Debug.Log("You blew up and you have "+health+" health left.");
                
                    if (health == 0)
                    {
                        CrashEffect();
                    }
                }
                break;





        }
    }

    void CrashEffect()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
        
    }

    void WinEffect()
    {
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
}
