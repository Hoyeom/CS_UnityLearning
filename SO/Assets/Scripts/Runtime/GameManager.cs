using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        SceneLoad();
    }

    public void SceneLoad()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += TestLoad;
    }

    private void TestLoad(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log($"{arg0.name} Scene Load");
        SceneManager.sceneLoaded -= TestLoad;
    }
}
