using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform groundPos;
    public Transform wallPos;
    public LayerMask groundLayer;
    public EnemyStats stats;
    public FloatReference gravity;
    public Sprite deathSprite;

    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float horizontalAxis;
    [HideInInspector]
    public float verticalAxis;
    [HideInInspector]
    public bool jump;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool walled;
    [HideInInspector]
    public bool wallSideLeft;
    [HideInInspector]
    public bool wallSideRight;

    //ground friction / movespeed
    //air friction / movespeed


    private void Awake()
    {
        moveSpeed = stats.moveSpeed;
        currentHealth = stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <=0)
        {
            Debug.Log(gameObject.name + " just died :(");
            //die and play animation
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = deathSprite;
            //currentSprite.sprite = deathSprite;
            Destroy(gameObject, .5f);
        }
        /*
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
        */
        grounded = Physics2D.OverlapBoxAll(groundPos.position, new Vector2(.5f*transform.localScale.x, 0.1f * transform.localScale.y), 0, groundLayer).Length > 0;
        var results = Physics2D.OverlapBoxAll(wallPos.position, new Vector2(1.1f, .5f), 0, groundLayer);
        walled = results.Length > 0;
        if (walled)
        {
            wallSideLeft = false;
            wallSideRight = false;
            Collider2D wall = results[0];
            if (wall.transform.position.x < gameObject.transform.position.x)
            {
                wallSideLeft = true;
            }
            else 
            {
                wallSideRight = true;
            }
        } 
       
    }

    internal void dealDamage(int damage)
    {
        Debug.Log(gameObject.name + " got hit for " + damage + " damage!");
        currentHealth -= damage;
        Debug.Log(gameObject.name + " is at " + currentHealth + " health!");
    }
}
