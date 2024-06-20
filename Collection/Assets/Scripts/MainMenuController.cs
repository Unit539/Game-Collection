using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string menuName;

    public void MainMenuButton()
    {
        SceneManager.LoadScene(menuName);
    }
}
