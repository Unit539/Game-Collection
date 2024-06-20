using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class GameManagerX : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public Button menuButton;
    public Button exitButton;
    public Button mainMenuButton;

    public List<GameObject> targetPrefabs;

    private float timer;
    public TextMeshProUGUI timerText;

    private AudioSource audioSource;
    public AudioClip clickSound;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f;
    private float minValueY = -3.75f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        timer = 60;
        UpdateScore(0);
        titleScreen.SetActive(false);
        exitButton.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer);
            if(timer < 0)
            {
                GameOver();
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        audioSource.PlayOneShot(clickSound, 1.0f);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void MenuButton()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        titleScreen.SetActive(true);
        slider.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
