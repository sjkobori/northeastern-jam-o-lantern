using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject bullet;
    Transform rangedStartPos;
    bool rangedAttack;

    [HideInInspector]
    int timeBetweenAttack;
    [HideInInspector]
    int projectileNum;

    // Start is called before the first frame update
    void Awake()
    {
        timeBetweenAttack = 0;
        projectileNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenAttack++;
        if(timeBetweenAttack >= 100)
        {
            projectileNum++;
            StartCoroutine("rangedShot");
            if (projectileNum == 5)
            {
                timeBetweenAttack = 0;
                projectileNum = 0;
            }
        }
        
    }
    IEnumerator rangedShot()
    {
        EnemyAIController pmc = gameObject.GetComponent<EnemyAIController>();
        float directionFacing = Mathf.Sign(transform.localScale.x);

        ProjectileController bulletController = Instantiate(bullet, new Vector3(directionFacing, 0, 0) + transform.position, Quaternion.identity).GetComponent<ProjectileController>();

        bulletController.dir = Vector2.right * directionFacing;
        yield return null;

    }
}