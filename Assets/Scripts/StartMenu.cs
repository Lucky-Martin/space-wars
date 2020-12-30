using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    SceneLoader sceneLoader;
    SceneFader sceneFader;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneFader = FindObjectOfType<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        StartOnClick();
    }

    private void StartOnClick()
    {
        if (sceneLoader && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Load");
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {

        float fadeTime = sceneFader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        sceneLoader.LoadGame();
    }
}
