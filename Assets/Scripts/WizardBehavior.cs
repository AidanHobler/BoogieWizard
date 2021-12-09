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

    private bool mark;
    private bool trigger;
    private bool cancel;

    private Rigidbody2D rb;
    private Animator anim;

    private Direction moveDirection;
    // TODO: Remove
    private float dt;

    private Vector2 rawInput;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        mark = false;
        trigger = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    /*
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
    */

    public void OnBeat()
    {
        SetMoveDirection(rawInput);

        if (moving)
        {
            transform.SetParent(FloorManager.instance.GetTileForMove(moveDirection).transform);
            transform.localPosition = Vector2.zero;
            moving = false;
        }

        if (mark)
        {
            FloorManager.instance.MarkTile();
            mark = false;
        }

        if (trigger)
        {
            FloorManager.instance.TriggerTiles();
            trigger = false;
        }

        if (cancel)
        {
            FloorManager.instance.ClearColumn();
            cancel = false;
        }

        anim.SetTrigger("Lean");
    }

    public void SetStickInput(Vector2 input)
    {
        rawInput = input;
    }

    public void Trigger()
    {
        trigger = true;
    }

    public void Mark()
    {
        mark = true;
    }

    public void Cancel()
    {
        cancel = true;
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


    public void DropMark()
    {

    }

    public void Activate()
    {

    }
}
