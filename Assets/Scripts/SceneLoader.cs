using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");       
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Core Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadWithDelay("Game Over", 1.5f));
    }

    IEnumerator LoadWithDelay(string scene, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(scene);
    }
}
