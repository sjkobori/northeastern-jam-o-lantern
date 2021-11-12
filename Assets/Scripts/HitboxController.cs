using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public bool colliding;
    public LayerMask layers;
    // Start is called before the first frame update
    void Start()
    {
        colliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliding = true;
        //Debug.Log("Hitbox colliding!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliding = false;
    }
}
