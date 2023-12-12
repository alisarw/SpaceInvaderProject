using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public Color[] healthColors;
    SpriteRenderer shieldSprite;
    int healthIndex;

    // Start is called before the first frame update
    void Start()
    {
        shieldSprite = GetComponent<SpriteRenderer>();
        healthIndex = healthColors.Length - 1;
        shieldSprite.color = healthColors[healthIndex];
    }

    public void TakeDamage()
    {
        healthIndex--;
        if (healthIndex < 0)
        {
            gameObject.SetActive(false);
            return;
        }
        shieldSprite.color = healthColors[healthIndex];
    }
}
