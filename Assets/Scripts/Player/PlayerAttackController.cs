using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public Transform meleeStartPos;
    public GameObject bullet;
    Transform meleeEndPos;
    bool meleeAttack;
    Transform rangedStartPos;
    bool rangedAttack;
    [SerializeField] private BoxCollider2D swordHitbox;
    [SerializeField] private AudioClip shootySound;
    [SerializeField] private AudioClip noShootySound;
    [SerializeField] private AudioClip slashySound;

    public float rangedCD;
    public float meleeCD;

    private float _currentMeleeCD;
    private float _currentRangedCD;

    public PlayerStats playerStats;

    private bool _meleeAttacking;
    private float _meleeAttackTime;
    private float _currentMeleeAttackTime;
    private List<EnemyAIController> hitList;
    
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        _currentMeleeCD = 0;
        _currentRangedCD = 0;
        _meleeAttackTime = 5/6f;
        _meleeAttacking = false;
        ResetHitlist();
        audioSrc = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCD();
   


        if (Input.GetButtonDown("Fire1") && _currentMeleeCD <= 0)
        {
            GetComponentInChildren<Animator>().SetTrigger("Slash");
            
            StartCoroutine(nameof(meleeStrike));
        } else if (Input.GetButtonDown("Fire2") && _currentRangedCD <= 0) {
            GetComponentInChildren<Animator>().SetTrigger("Shoot");
            StartCoroutine(nameof(rangedShot));
        }
    }

    private void FixedUpdate()
    {
        if (_meleeAttacking)
        {
            _currentMeleeAttackTime += Time.fixedDeltaTime;
            if (_currentMeleeAttackTime > _meleeAttackTime)
            {
                _meleeAttacking = false;
            }
            CheckSwordHitbox();
        } else
        {
            _currentMeleeAttackTime = 0f;
            ResetHitlist();
        }
    }

    private void CheckSwordHitbox()
    {
        List<Collider2D> attackHits = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyLayer);
        swordHitbox.OverlapCollider(filter, attackHits);

        if (attackHits.Count > 0)
        {
            EnemyAIController enemyHit;
            foreach (Collider2D c in attackHits)
            {
                enemyHit = c.gameObject.GetComponent<EnemyAIController>();
                if (!hitList.Contains(enemyHit))
                {
                    playerStats.resetAmmo();
                    enemyHit.dealDamage(10);
                    Debug.Log("Enemy Hit");
                    hitList.Add(enemyHit);
                }
            }

        }
    }

    private void ResetHitlist()
    {
        hitList = new List<EnemyAIController>();
        _currentMeleeAttackTime = 0;
    }

    IEnumerator meleeStrike()
    {
        RefreshMeleeCD();
        gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(slashySound, 0.5f);
        _meleeAttacking = true;
       
       yield return new WaitForSeconds(.05f);
    }

    IEnumerator rangedShot()
    {
        RefreshRangedCD();
        if (playerStats.ammo > 0)
        {
            playerStats.ammo--;
            PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
            float directionFacing = Mathf.Sign(pmc.facing.x);
            audioSrc.PlayOneShot(shootySound, 0.25f);
       
            ProjectileController bulletController = Instantiate(bullet, new Vector3(directionFacing, 0, 0) + transform.position, Quaternion.identity).GetComponent<ProjectileController>();

            bulletController.dir = Vector2.right * directionFacing;
        } else
        {
            audioSrc.PlayOneShot(noShootySound, 0.25f);

        }
        
         yield return null;
        
    }

    private void RefreshMeleeCD()
    {
        _currentMeleeCD = meleeCD;
        
    }

    private void RefreshRangedCD()
    {
        _currentRangedCD = rangedCD;
    }

    private void UpdateCD()
    {
        if (_currentMeleeCD > 0)
        {
            _currentMeleeCD -= Time.deltaTime;
        }
        if (_currentRangedCD > 0)
        {
            _currentRangedCD -= Time.deltaTime;
        }
    }
}
