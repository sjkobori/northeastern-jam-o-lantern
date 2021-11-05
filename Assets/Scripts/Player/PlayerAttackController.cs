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
    [SerializeField] private AudioClip shootySound;
    [SerializeField] private AudioClip noShootySound;
    [SerializeField] private AudioClip slashySound;

    public float rangedCD;
    public float meleeCD;

    private float _currentMeleeCD;
    private float _currentRangedCD;

    public PlayerStats playerStats;



    // Start is called before the first frame update
    void Awake()
    {
        _currentMeleeCD = 0;
        _currentRangedCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCD();

        if (Input.GetKeyDown(KeyCode.Z) && _currentMeleeCD <= 0)
        {
            GetComponentInChildren<Animator>().SetTrigger("Slash");
            StartCoroutine(nameof(meleeStrike));
        } else if (Input.GetKeyDown(KeyCode.X) && _currentRangedCD <= 0) {
            GetComponentInChildren<Animator>().SetTrigger("Shoot");
            StartCoroutine(nameof(rangedShot));
        }
    }

    IEnumerator meleeStrike()
    {
        RefreshMeleeCD();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        float directionFacing = Mathf.Sign(pmc.facing.x);
        
        gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(slashySound, 0.5f);


        List<EnemyAIController> hitList = new List<EnemyAIController>();


        Vector2 meleePos = (Vector2)transform.position + new Vector2(.75f, 0) * directionFacing;
        float radius = 1.25f;
        var attackHits = Physics2D.OverlapCircleAll(meleePos, radius, enemyLayer);
   
            if (attackHits.Length > 0)
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
            gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(shootySound, 0.25f);
       
            ProjectileController bulletController = Instantiate(bullet, new Vector3(directionFacing, 0, 0) + transform.position, Quaternion.identity).GetComponent<ProjectileController>();

            bulletController.dir = Vector2.right * directionFacing;
        } else
        {
            gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(noShootySound, 0.25f);

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
