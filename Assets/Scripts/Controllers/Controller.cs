using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class Controller : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference move_input;
    public InputActionReference jump_input;

    public Coroutine c_movement;
    public Coroutine c_jump;

    protected Vector2 direction;
    protected Vector2 speed;
    protected float jump_velocity;
    protected Rigidbody2D rb;

    protected void Awake()
    {
        // Movement
        move_input.action.started += MoveInput;
        move_input.action.canceled += MoveInputCanceled;

        /*jump_input.action.performed += JumpInput;
        jump_input.action.canceled += JumpInputCanceled;*/
    }


    protected void OnDestroy()
    {
        move_input.action.started -= MoveInput;
        move_input.action.canceled -= MoveInputCanceled;

        /*jump_input.action.performed -= JumpInput;*/
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

    protected virtual void MoveInputCanceled(InputAction.CallbackContext obj)
    {
        if (c_movement == null)
            return;

        StopCoroutine(c_movement);
        c_movement = null;
        direction = ControllerHelper.PrepareDirection(Vector2.zero);
    }

    protected void JumpInput(InputAction.CallbackContext obj)
    {
        if (c_jump == null)
            return;

        StopCoroutine(c_jump);
        c_jump = null;

        Debug.Log("jump");
        rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
    }

    private void JumpInputCanceled(InputAction.CallbackContext obj)
    {
        if (c_jump != null)
            return;

        float hold_duration = Mathf.Clamp((float) obj.duration, 0, 1);
        Debug.Log(hold_duration);

        Vector2 vector2 = new Vector2(0, hold_duration * 100);
        GameObject player = PlayerHelper.GetPlayer().gameObject;

        c_jump = StartCoroutine(JumpTrackingRoutine());
        IEnumerator JumpTrackingRoutine()
        {
            while (true)
            {
                EntityHelper.GetEntityRigidbody(player).AddForce(EntityHelper.GetForwardDirection(player) * vector2 * (float)hold_duration * Time.fixedDeltaTime, ForceMode2D.Impulse);

                yield return null;
            }
        }
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

    public void ModifySpeed(Vector2 newVector2)
    {
        Vector2 v;
        v.x = Mathf.Clamp((speed + newVector2).x, PlayerHelper.GetPlayerData().player_min_speed, PlayerHelper.GetPlayerData().player_max_speed);
        v.y = Mathf.Clamp((speed + newVector2).y, PlayerHelper.GetPlayerData().player_min_speed, PlayerHelper.GetPlayerData().player_max_speed);

        speed = v;
    }
}
