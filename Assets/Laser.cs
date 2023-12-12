using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveSpeed;
    public bool isPlayerLaser;

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        if (isPlayerLaser)
        {
            if (collision.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            if (collision.CompareTag("Shield"))
            {
                collision.GetComponent<ShieldHealth>().TakeDamage();
            }
        }
        if (collision.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
