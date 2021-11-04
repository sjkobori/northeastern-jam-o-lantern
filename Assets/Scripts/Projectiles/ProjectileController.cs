using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public LayerMask hitList;
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

    private void OnTriggerEnter2D(Collider2D collision) {
        // Return if we didn't hit anything we would have collided with
        if (hitList != 0 && (hitList & (1 << collision.gameObject.layer)) == 0) return;

        EnemyAIController ec;
        collision.gameObject.TryGetComponent(out ec);
        if(ec) ec.dealDamage(damage);

        //Play animation
        Debug.Log(collision.gameObject.name + " got hit by " + gameObject.name);
        //die and play animation
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = explosionSprite;
        //currentSprite.sprite = deathSprite;
        Destroy(gameObject, .5f);
        
    }
}
