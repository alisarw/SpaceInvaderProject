using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyScore;
    public AudioClip deathSound;
    EnemyParent enemyParent;
    AudioSource audioSource;

    private void Start()
    {
        enemyParent = GetComponentInParent<EnemyParent>();
        audioSource = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyParent.manager.GameOver();
            Debug.Log("Lose");
        }
        if (collision.CompareTag("Laser"))
        {
            if (collision.GetComponent<Laser>().isPlayerLaser)
            {
                audioSource.PlayOneShot(deathSound);
                gameObject.SetActive(false);
                enemyParent.RemoveEnemy(gameObject, enemyScore);
            }
        }
        if (collision.CompareTag("Boundary"))
        {
            enemyParent.manager.GameOver();
            Debug.Log("Lose");
        }
    }
}
