using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [HideInInspector]
    public bool colliding;
    [HideInInspector]
    public Vector2 collisionCenter;
    public LayerMask layers;
    // Start is called before the first frame update
    [SerializeField]
    private Collider2D collider2D;
    void Start()
    {
        colliding = false;
    }

    private void Update()
    {
        if (!collider2D.IsTouchingLayers(layers))
        {
            colliding = false;
            collisionCenter = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        colliding = true;
        collisionCenter = collision.transform.position;
        //Debug.Log("Hitbox colliding!");
    }
}
