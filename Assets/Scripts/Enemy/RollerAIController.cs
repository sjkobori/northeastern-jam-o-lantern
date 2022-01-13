using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAIController : EnemyAIController
{


    private Collider2D[] hitColliders;

    [HideInInspector]
    public Vector2 facing;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Face(Vector2.right);
        base.Awake();
    }


    public void Face(Vector2 direction)
    {
        facing = direction;

        Transform tf = spriteRenderer.transform;

        var scale = tf.localScale;
        tf.localScale = new Vector3(
            Mathf.Sign(facing.x) * Mathf.Abs(scale.x),
            Mathf.Sign(facing.y) * Mathf.Abs(scale.y),
            scale.z
        );
    }
}
