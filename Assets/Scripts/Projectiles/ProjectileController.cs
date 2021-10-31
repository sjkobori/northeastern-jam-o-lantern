using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public LayerMask[] hitList;
    public float speed;
    public int damage;
    public Vector2 dir;
    public Sprite explosionSprite;

    private Rigidbody2D rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAIController ec = collision.gameObject.GetComponent<EnemyAIController>();
            ec.dealDamage(damage);
        }

              //Play animation
            Debug.Log(collision.gameObject.name + " got hit by " + gameObject.name);
            //die and play animation
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = explosionSprite;
            //currentSprite.sprite = deathSprite;
            Destroy(gameObject, .5f);
        
    }
}
