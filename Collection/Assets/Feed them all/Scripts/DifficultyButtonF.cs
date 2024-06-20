using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonF : MonoBehaviour
{
    private SpawnManagerF spawnManager;
    private Button button;

    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerF>();
        button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        spawnManager.StartGame(difficulty);
    }
}
