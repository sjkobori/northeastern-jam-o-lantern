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

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //meleeAttack = 

        //rangedAttack = Input.GetButton("RangedAttack");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetComponentInChildren<Animator>().SetTrigger("Slash");
            StartCoroutine(nameof(meleeStrike));
        } else if (Input.GetKeyDown(KeyCode.X)) {
            GetComponentInChildren<Animator>().SetTrigger("Shoot");
            StartCoroutine(nameof(rangedShot));
        }
    }

    IEnumerator meleeStrike()
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        float directionFacing = Mathf.Sign(pmc.facing.x);
        
        gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(slashySound, 0.5f);


        List<EnemyAIController> hitList = new List<EnemyAIController>();
        float mag = new Vector2(0, 1.75f).magnitude;


        for (float deg = Mathf.PI/2; deg >= -Mathf.PI/4 ; deg += -0.2f)
        {
            var hit = Physics2D.Raycast(transform.position, 
                new Vector2(Mathf.Cos(deg) * mag * directionFacing, Mathf.Sin(deg) * mag), 1, enemyLayer);
            Debug.DrawRay(transform.position,
                new Vector2(Mathf.Cos(deg) * mag * directionFacing, Mathf.Sin(deg) * mag), Color.red, .5f);
            if (hit)
            {
                EnemyAIController enemyHit = hit.collider.gameObject.GetComponent<EnemyAIController>();
                if (!hitList.Contains(enemyHit)) {
                    enemyHit.dealDamage(10);
                    Debug.Log("Enemy Hit");
                    hitList.Add(enemyHit);
                }
                
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator rangedShot()
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        float directionFacing = Mathf.Sign(pmc.facing.x);

        gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(shootySound, 0.25f);
        // gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(noShootySound, 0.25f);

        ProjectileController bulletController = Instantiate(bullet, new Vector3(directionFacing,0,0) + transform.position, Quaternion.identity).GetComponent<ProjectileController>();

        bulletController.dir = Vector2.right * directionFacing;
         yield return null;
        
    }
}
