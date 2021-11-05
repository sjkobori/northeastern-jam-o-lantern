using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private FloatReference iFrames;
    private float invincibility;
    public Transform lastRespawnPoint;

    /// <summary>
    /// controls taking dmg
    /// dying
    /// respawning
    /// </summary>
    // Start is called before the first frame update
    void Awake()
    {
        invincibility = iFrames.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        }
        
        if (playerStats.health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnPointController rpc;
        if (collision.gameObject.TryGetComponent(out rpc))
        {
            Debug.Log("Player got a respawn point: " + collision.gameObject.name);
            lastRespawnPoint = rpc.transform;
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
                playerStats.health--;
                invincibility = iFrames.value;
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
