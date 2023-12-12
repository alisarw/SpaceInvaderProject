using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed;
    public float xLimit;
    public GameObject laserPrefab;
    public AudioClip laserSound;
    public AudioSource source;

    private GameObject currentLaser;

    // Update is called once per frame
    void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");

        Vector3 movePosition = transform.position;

        movePosition.x += moveDirection * moveSpeed * Time.deltaTime;

        movePosition.x = Mathf.Clamp(movePosition.x, -xLimit, xLimit);

        transform.position = movePosition;

        if (currentLaser == null && Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)  
        {
            source.PlayOneShot(laserSound);
            currentLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            if(collision.GetComponent<Laser>().isPlayerLaser == false)
            {
                gameManager.GameOver();
            }
        }
    }

}
