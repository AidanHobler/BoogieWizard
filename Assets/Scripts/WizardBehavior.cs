using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class WizardBehavior : MonoBehaviour
{

    private OSC osc;

    [SerializeField]
    private float MoveSpeed;

    private bool moving;
    private Vector3 direction;
    private Rigidbody2D rb;

    private Direction moveDirection;
    // TODO: Remove
    private float dt;

    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        direction = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        gamepad = Gamepad.current;

    }
    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        if (dt > 0.5f)
        {
            OnBeat();
            dt = 0.0f;
        }
    }

    public void OnBeat()
    {
        Vector2 move = gamepad.leftStick.ReadValue();

        SetMoveDirection(move);

        if (moving)
        {
            transform.SetParent(FloorManager.instance.GetTileForMove(moveDirection).transform);
            transform.localPosition = Vector2.zero;
            moving = false;
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        if (direction.magnitude > 0.2f)
        {
            moving = true;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x < 0)
                {
                    moveDirection = Direction.Left;
                }
                else
                {
                    moveDirection = Direction.Right;
                }
            }
            else
            {
                if (direction.y < 0)
                {
                    moveDirection = Direction.Down;
                }
                else
                {
                    moveDirection = Direction.Up;
                }

            }

        }


    }

    public void StopMoving()
    {
        // moving = false;
    }

    public void DropMark()
    {

    }

    public void Activate()
    {

    }
}
