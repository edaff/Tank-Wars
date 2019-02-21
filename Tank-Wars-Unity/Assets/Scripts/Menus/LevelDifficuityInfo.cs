using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDifficuityInfo : MonoBehaviour
{
    [SerializeField] int currentSceneIndex;                              //this will be used to keep track of scene, if > 1 delete this game object

    [SerializeField] int difficuity;                                //this will be passed in tankSelect

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //this will delect because it only needs the info for tank select
        if (currentSceneIndex > 1)
        {
            Destroy(gameObject);
        }

    }


    public void Easy()
    {
        difficuity = 1;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Medium()
    {
        difficuity = 2;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Hard()
    {
        difficuity = 3;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public int GetDifficuity()
    {
        return difficuity;
    }
}
