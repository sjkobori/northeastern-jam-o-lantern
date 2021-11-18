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
    [SerializeField] private List<AudioClip> hurtSounds;

    private HitboxController _hitbox;
    [HideInInspector]
    public Vector2 enemyCenter;
    [HideInInspector]
    public bool takingDamage;

    private AudioSource audioSrc;

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
        audioSrc = GetComponentInChildren<AudioSource>();
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
    }

    private void CheckForDamage()
    {
        takingDamage = false;
        if (_hitbox.colliding &&  invincibility < 0)
        {
            TakeDamage();
            enemyCenter = _hitbox.collisionCenter;
        }
    }

    private void TakeDamage()
    {
        playerStats.health--;
        invincibility = iFrames.value;
        takingDamage = true;
        
        if (playerStats.health <= 0)
        {
            Die();
        }
        else {
            PlayHurtSound();
        }
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
        EnemyAIController ec;
        ProjectileController pc;
        HazardController hc;
        if (collision.gameObject.TryGetComponent(out hc)) {
            Debug.Log("Player got hit by: " + collision.gameObject.name);
            Die();
        }
        else if (invincibility <= 0 &&
                 (collision.gameObject.TryGetComponent(out ec) ||
                  collision.gameObject.TryGetComponent(out pc)))
        {
            Debug.Log("Player got hit by: " + collision.gameObject.name);
            TakeDamage();
        }
    }

    private void Die() {
        PlayHurtSound();
        Respawn();
    }

    private void PlayHurtSound() {
        AudioClip clip = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Count)];
        audioSrc.PlayOneShot(clip, 0.4f);
    }

    private void Respawn()
    {
        //reset health
        playerStats.resetHealth();
        //go back to last spawn point
        transform.position = lastRespawnPoint.position;
    }

}
