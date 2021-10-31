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
    public bool jump;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool walled;
    [HideInInspector]
    public bool wallSideLeft;
    [HideInInspector]
    public bool wallSideRight;


    // Update is called once per frame
    void Update()
    {

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        grounded = Physics2D.OverlapBoxAll(groundPos.position, new Vector2(.5f, 0.1f), 0, groundLayer).Length > 0;
        wallSideLeft = Physics2D.OverlapBoxAll(leftWallPos.position, new Vector2(.1f, .5f), 0, groundLayer).Length > 0;
        wallSideRight = Physics2D.OverlapBoxAll(rightWallPos.position, new Vector2(.1f, .5f), 0, groundLayer).Length > 0;
        walled = wallSideLeft || wallSideRight;
        
        
        
       
    }

   
}
