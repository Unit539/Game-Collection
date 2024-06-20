using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManagerF : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    public GameObject titleScreen;
    public Button mainMenuButton;
    public Button exitButton;
    public Button restartButton;
    public Button menuButton;

    private AudioSource audioSound;
    public AudioClip clickSound;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public GameObject[] animalPrefabs;

    [SerializeField] private float spawnX = 19f;
    [SerializeField] private float spawnZ = 19f;

    private float startDelay = 2;
    private float spawnInterval = 1.5f;

    private int score;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        audioSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartGame(int difficulty)
    {
        Time.timeScale = 1f;
        isGameActive = true;
        score = 0;
        spawnInterval /= difficulty;

        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        UpdateScore(0);

        titleScreen.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        audioSound.PlayOneShot(clickSound, 1f);
    }

    void SpawnRandomAnimal()
    {
        if (isGameActive)
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length);

            Vector3 spawnPos = new Vector3(Random.Range(spawnX, -spawnX), 0, spawnZ);

            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        }
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void MenuButton()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        titleScreen.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
