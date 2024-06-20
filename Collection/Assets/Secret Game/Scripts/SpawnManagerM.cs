using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManagerM : MonoBehaviour
{
    private PlayerControllerM playerControllerScript;

    public GameObject titleText;
    public GameObject obstaclePrefab;

    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public Button mainMenuButton;
    public Button exitButton;

    public AudioMixer audioMixer;
    public Slider slider;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerM>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        
        titleText.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject .SetActive(true);
    }

    public void SetVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
