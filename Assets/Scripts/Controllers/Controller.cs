using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference move_input;
    public InputActionReference jump_input;

    public Coroutine c_movement;

    protected Vector2 direction;
    protected Vector2 speed;
    protected float jump_velocity;
    protected Rigidbody2D rb;

    protected void Awake()
    {
        // Movement
        move_input.action.started += MoveInput;
        move_input.action.canceled += MoveCanceled;

        jump_input.action.started += JumpInput;
    }

    protected void OnDestroy()
    {
        move_input.action.started -= MoveInput;
        move_input.action.canceled -= MoveCanceled;

        jump_input.action.started -= JumpInput;
    }

    protected void FixedUpdate()
    {
        if (!rb)
            return;

        float x = rb.position.x + (direction.x * speed.x) * Time.fixedDeltaTime;
        float y = rb.position.y;

        rb.MovePosition(new Vector2(x, y));
    }

    protected virtual void MoveInput(InputAction.CallbackContext obj)
    {
        if (!rb)
            return;

        if (c_movement != null)
            return;

        c_movement = StartCoroutine(MovementTrackingRoutine());
        IEnumerator MovementTrackingRoutine()
        {
            while (true)
            {
                direction = ControllerHelper.PrepareDirection(obj.ReadValue<Vector2>());

                yield return null;
            }
        }
    }

    protected virtual void MoveCanceled(InputAction.CallbackContext obj)
    {
        if (c_movement == null)
            return;

        StopCoroutine(c_movement);
        c_movement = null;
        direction = ControllerHelper.PrepareDirection(Vector2.zero);
    }

    protected void JumpInput(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
    }

    public Vector2 GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(Vector2 newValue)
    {
        speed = newValue;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector2 newValue)
    {
        direction = newValue;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }
}
