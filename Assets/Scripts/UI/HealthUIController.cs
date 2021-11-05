using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour {

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Sprite healthSprite;

    private int _health;
    private GameObject heart;

    void Awake() {
        heart = new GameObject();
        heart.name = "heart";
        Image newImg = heart.AddComponent<Image>();
        newImg.sprite = healthSprite;
    }
    
    void Update() {
        if (_health == playerStats.health) return;

        if (_health < playerStats.health) {
            for (int i = 0; i < playerStats.health - _health; i++) {
                Instantiate(heart, transform);
            }
        } else {
            int toDestroy = _health - playerStats.health;
            foreach (Transform child in transform) {
                if (toDestroy == 0) break;
                Destroy(child.gameObject);
                toDestroy--;
            }
        }

        _health = playerStats.health;
    }
}
