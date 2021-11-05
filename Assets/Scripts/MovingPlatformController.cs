using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    public Transform[] patrolAreas;
    public float patrolTime;
    private float currentTime;

    private List<Vector2> _startingPartolAreas;
    private int _targetPosIndex;
    private Vector2 _targetPosition;
    private Vector2 _lastPosition;
    private Vector2 currentVel;
    void Awake()
    {
        _lastPosition = transform.position;
        _targetPosIndex = 0;
        currentVel = Vector2.zero;
        _startingPartolAreas = new List<Vector2>();
        foreach (Transform t in patrolAreas)
        {
            _startingPartolAreas.Add(t.position);
        }
        _targetPosition = _startingPartolAreas[_targetPosIndex];
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (((Vector2)transform.position - _targetPosition).magnitude < Mathf.Epsilon)
        {

            setNextPos();
        }
        
        //go towards target position
        transform.position = Vector2.Lerp(_lastPosition, _targetPosition, currentTime/patrolTime);

    }

    private void setNextPos()
    {
        _targetPosIndex = (_targetPosIndex + 1) % _startingPartolAreas.Count;
        _lastPosition = _targetPosition;
        _targetPosition = _startingPartolAreas[_targetPosIndex];
        currentTime = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerMovementController pmc;

        if (collision.gameObject.TryGetComponent(out pmc)) {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerMovementController pmc;

        if (collision.gameObject.TryGetComponent(out pmc))
        {
            collision.transform.parent = null;
        }
    }
}
