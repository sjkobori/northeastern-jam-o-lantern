using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Transform groundPos;
    public Transform leftWallPos;
    public Transform rightWallPos;
    public LayerMask groundLayer;

    public FloatReference groundMoveSpeed;
    public FloatReference airMoveSpeed;
    public FloatReference jumpSpeed;

    public FloatReference maxHorizontalSpeed;
    public FloatReference maxVerticalSpeed;
    public FloatReference wallSlideSpeedRatio;

    public FloatReference gravity;
    public FloatReference jumpGravity;

    [HideInInspector]
    public float horizontalAxis;
    [HideInInspector]
    public float verticalAxis;
    [HideInInspector]
    public bool jumpHeld;
    public Trigger jump = new Trigger();
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool walled;
    [HideInInspector]
    public bool wallSideLeft;
    [HideInInspector]
    public bool wallSideRight;
    [HideInInspector]
    public Vector2 facing;

    private SpriteRenderer spriteRenderer;


    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Face(Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        jump.Set(Input.GetButtonDown("Jump"));
        jumpHeld = Input.GetButton("Jump");

        if (Mathf.Abs(horizontalAxis) > Mathf.Epsilon) {
            facing = Vector2.right * Mathf.Sign(horizontalAxis);
        }

        grounded = Physics2D.OverlapBoxAll(groundPos.position, new Vector2(.5f, 0.1f), 0, groundLayer).Length > 0;
        wallSideLeft = Physics2D.OverlapBoxAll(leftWallPos.position, new Vector2(.1f, .5f), 0, groundLayer).Length > 0;
        wallSideRight = Physics2D.OverlapBoxAll(rightWallPos.position, new Vector2(.1f, .5f), 0, groundLayer).Length > 0;
        walled = wallSideLeft || wallSideRight;
    }

    public void Face(Vector2 direction) {
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
