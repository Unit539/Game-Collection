using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOfBounds : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;

    private SpawnManagerF spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerF>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if(transform.position.z < lowerBound)
        {
            spawnManager.GameOver();
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }
}
