using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretCode : MonoBehaviour
{
    private KonamiCode konamiCode;

    // Start is called before the first frame update
    void Awake()
    {
        konamiCode = GetComponent<KonamiCode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (konamiCode.success)
        {
            SceneManager.LoadScene("Secret");
        }
    }
}
