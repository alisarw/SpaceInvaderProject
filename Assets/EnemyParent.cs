using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public GameManager manager;
    public GameObject[] enemyPrefabs;
    public GameObject enemyLaser;
    public int columns;
    public int rows;

    public AnimationCurve moveSpeed;
    public float xLimit;
    public float yDownVal;
    public float laserRate;

    public AudioClip laserSound;
    public AudioSource source;

    float moveDirection = 1f;
    List<GameObject> enemyList;

    float laserDelay;

    int totalEnemy;
    int currentKilled;
    float killedPercent;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGrid();
        laserDelay = laserRate;
    }

    private void Update()
    {
        killedPercent = (float)currentKilled / (float)totalEnemy;
        Vector3 newPos = transform.position;
        newPos.x += moveDirection * moveSpeed.Evaluate(killedPercent) * Time.deltaTime;
        transform.position = newPos;

        //Enemy stays within boundary
        foreach (Transform enemy in transform)
        {
            if (enemy.gameObject.activeSelf)
            {
                if (enemy.position.x >= xLimit && moveDirection == 1)
                {
                    MoveDown();
                    break;
                }
                else if (enemy.position.x <= -xLimit && moveDirection == -1)
                {
                    MoveDown();
                    break;
                }
            }         
        }

        //Enemy Shoots Laser
        if(enemyList.Count > 0)
        {
            if (laserDelay <= 0)
            {
                laserDelay = laserRate;
                ShootLaser();
            }
            else
            {
                laserDelay -= Time.deltaTime;
            }
        }       
    }

    public void MoveDown()
    {
        moveDirection *= -1;
        transform.position -= new Vector3(0f, yDownVal, 0f);
    }


    void EnemyGrid()
    {
        float startingPositionX = -2.8f;
        float startingPositionY = 0f;

        enemyList = new List<GameObject>();

        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                GameObject temp = Instantiate(enemyPrefabs[j], transform);
                enemyList.Add(temp);
                temp.transform.localPosition = new Vector3(startingPositionX, startingPositionY, 0f);
                startingPositionX += 0.7f;
            }
            startingPositionX = -2.8f;
            startingPositionY += 0.7f;
        }
        totalEnemy = enemyList.Count;
    }

    void ShootLaser()
    {
        int randomInt = Random.Range(0, enemyList.Count);
        GameObject laser = Instantiate(enemyLaser, enemyList[randomInt].transform.position, Quaternion.identity);
        source.PlayOneShot(laserSound);
    }

    public void RemoveEnemy(GameObject enemy, int score)
    {
        currentKilled++;
        enemyList.Remove(enemy);
        manager.UpdateScore(score);
        if(enemyList.Count <= 0)
        {
            manager.Win();
        }
    }    

}
