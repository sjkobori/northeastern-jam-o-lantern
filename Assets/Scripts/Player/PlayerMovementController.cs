using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Transform groundPos;
    public Transform wallPos;
    public LayerMask groundLayer;
    public FloatReference initialMoveSpeed;
    public FloatReference maxMoveSpeed;
    public FloatReference gravity;
    public FloatReference jumpGravity;
    public FloatReference airFriction;
    public FloatReference groundFriction;

    [HideInInspector]
    public float moveSpeed;
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

    //ground friction / movespeed
    //air friction / movespeed


    private void Awake()
    {
        moveSpeed = initialMoveSpeed.value;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
        
        grounded = Physics2D.OverlapBoxAll(groundPos.position, new Vector2(.5f, 0.1f), 0, groundLayer).Length > 0;
        walled = Physics2D.OverlapBoxAll(wallPos.position, new Vector2(1.1f, .5f), 0, groundLayer).Length > 0;
    }

   
}
