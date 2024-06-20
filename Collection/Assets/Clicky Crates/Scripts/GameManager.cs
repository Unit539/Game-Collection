using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public Button menuButton;
    public Button exitButton;
    public Button mainMenuButton;

    private AudioSource audioSound;
    public AudioClip clickSound;

    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;

    private void Start()
    {
        audioSound = GetComponent<AudioSource>();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        audioSound.PlayOneShot(clickSound, 1.0f);
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.SetActive(false);
        slider.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void MainMenu()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        exitButton.gameObject.SetActive(true);
        titleScreen.SetActive(true);
        slider.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
