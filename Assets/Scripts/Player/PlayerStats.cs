using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] 
    private int baseHealth;            
    [HideInInspector]
    public int health;
    [SerializeField]
    private int baseAmmo;
    [HideInInspector]
    public int ammo;
    [HideInInspector]
    public Vector2 playerPos;

    private void OnEnable()
    {
        playerPos = new Vector2(0, 0);
        resetHealth();
        resetAmmo();
    }

    public void resetHealth()
    {
        health = baseHealth;
    }

    public void resetAmmo()
    {
        ammo = baseAmmo;
    }
}