using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SpawnManager : MonoBehaviour
{
    private GameObject player;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI titleText;

    public Button restartButton;
    public Button exitButton;
    public Button menuButton;
    public Button mainMenuButton;

    public AudioMixer audioMixer;
    public Slider slider;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 0;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (isGameActive)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
                SpawnPowerup();
            }
            UpdateWavesText(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateRandomPos(), powerupPrefab.transform.rotation);
    }

    public Vector3 GenerateRandomPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    private void UpdateWavesText(int waves)
    {
        wavesText.text = "Waves: " + waves;
    }


    public void GameOver()
    {
        isGameActive = false;
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;

        titleText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        waveNumber = 0;
        player.transform.position = new Vector3(0, 0.13f, 0);

        Time.timeScale = 1f;
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void MenuButton()
    {
        titleText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);

        gameoverText.gameObject.SetActive(false);
        restartButton.gameObject .SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
