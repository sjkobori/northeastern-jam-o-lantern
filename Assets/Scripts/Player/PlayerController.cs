using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private FloatReference iFrames;
    private float invincibility;
    public Transform lastRespawnPoint;

    private HitboxController _hitbox;

    /// <summary>
    /// controls taking dmg
    /// dying
    /// respawning
    /// </summary>
    // Start is called before the first frame update
    void Awake()
    {
        playerStats.playerPos = transform.position;
        invincibility = iFrames.value;
        _hitbox = GetComponentInChildren<HitboxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }

        CheckForDamage();
        
        playerStats.playerPos = transform.position;
        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        }
        
        if (playerStats.health <= 0)
        {
            Die();
        }
    }

    private void CheckForDamage()
    {
        if (_hitbox.colliding &&  invincibility < 0)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        playerStats.health--;
        invincibility = iFrames.value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnPointController rpc;
        LoadingZoneController lzc;
        if (collision.gameObject.TryGetComponent(out rpc))
        {
            Debug.Log("Player got a respawn point: " + collision.gameObject.name);
            lastRespawnPoint = rpc.transform;
        }
        if (collision.gameObject.TryGetComponent(out lzc))
        {
            Debug.Log("Player hit a loading zone: " + collision.gameObject.name);
            SceneManager.LoadScene("CreditsScene");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Player got hit by: " + collision.gameObject.name);
       // playerStats.health--;
        //if hitting enemy or hazard && noIframes
        
        if (invincibility < 0)
        {
            EnemyAIController ec;
            ProjectileController pc;
            HazardController hc;
            if ((collision.gameObject.TryGetComponent(out ec) ||
            collision.gameObject.TryGetComponent(out pc) ||
            collision.gameObject.TryGetComponent(out hc)))
            {
                Debug.Log("Player got hit by: " + collision.gameObject.name);
                TakeDamage();
                if (collision.gameObject.TryGetComponent(out hc))
                {
                    Respawn();
                }
            }
        }
       
    }

    private void Die()
    {
        //reset health
        playerStats.resetHealth();
        //go back to respawn
        Respawn();
    }

    private void Respawn()
    {
        transform.position = lastRespawnPoint.position;
    }

}
