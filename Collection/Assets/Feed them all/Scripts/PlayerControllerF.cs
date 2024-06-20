using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerF : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float xBound = 20.0f;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Shoot();
    }

    private void XBound()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }

    private void PlayerMovement()
    {
        float horizontlaInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontlaInput * Time.deltaTime * speed);

        XBound();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
