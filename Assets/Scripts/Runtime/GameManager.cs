using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void SceneLoad()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += TestLoad;
    }

    private void TestLoad(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= TestLoad;
    }
}
