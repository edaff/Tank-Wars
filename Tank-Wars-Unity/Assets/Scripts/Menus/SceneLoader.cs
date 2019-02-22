using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int loadTime = 2;             //this var will handle load time if needed

    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    //function used for waiting x ammount of seconds where x = int load time
    IEnumerator LoadTimeBetweenScenes()
    {
        yield return new WaitForSeconds(loadTime);
    }

    //function used to loading next scene
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    //function used to exit game
    public void QuitGame()
    {
        Application.Quit();
    }

    //function used to pladed main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //function used to load 10 by 10 grind player vs player level
    public void Load10x10SelectScreen()
    {
        SceneManager.LoadScene(1);
    }

    //function used to load game over screen
    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("Game Over");
    }


}
