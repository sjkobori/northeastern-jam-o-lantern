using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour {

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Sprite[] healthSprites;
    // [SerializeField] private Sprite healthSprite;

    private int _health;
    private Image image;

    void Awake() {
        image = GetComponent<Image>();
    }
    
    void Update() {
        if (_health == playerStats.health) return;

        image.sprite = healthSprites[playerStats.health - 1];
        _health = playerStats.health;
    }
}
