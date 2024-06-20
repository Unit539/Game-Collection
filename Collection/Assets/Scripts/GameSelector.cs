using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour
{
    public string scenesName;
    
    public void OpenGame()
    {
        SceneManager.LoadScene(scenesName);
    }
}
