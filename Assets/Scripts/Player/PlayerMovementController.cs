using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Transform groundPos;
    public Transform wallPos;
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

   
}
