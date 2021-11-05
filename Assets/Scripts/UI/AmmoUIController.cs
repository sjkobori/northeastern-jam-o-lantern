using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIController : MonoBehaviour {

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Sprite ammoSprite;

    private int _ammo;
    private GameObject ammo;

    void Awake() {
        ammo = new GameObject();
        ammo.name = "bullet";
        Image newImg = ammo.AddComponent<Image>();
        newImg.sprite = ammoSprite;
    }
    
    void Update() {
        if (_ammo == playerStats.ammo) return;

        if (_ammo < playerStats.ammo) {
            for (int i = 0; i < playerStats.ammo - _ammo; i++) {
                Instantiate(ammo, transform);
            }
        } else {
            int toDestroy = _ammo - playerStats.ammo;
            foreach (Transform child in transform) {
                if (toDestroy == 0) break;
                Destroy(child.gameObject);
                toDestroy--;
            }
        }

        _ammo = playerStats.ammo;
    }
}
