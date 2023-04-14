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
    public Vector2 direction;
    public Vector2 speed;
    public float jump_velocity;
    public Rigidbody2D rb;

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

   /* public void Params(params string[] list)
    {
        Debug.Log("HERE");
        foreach (string name in list)
        {
            Debug.Log(name);
            _animatorController.TriggerAnimation(name, false);
        }
    }*/

    protected virtual void MoveInput(InputAction.CallbackContext obj)
    {
        /*if (_isJumping)
            return;*/

        if (!rb)
            return;

        if (c_movement != null)
            return;

        c_movement = StartCoroutine(MovementTrackingRoutine());
        IEnumerator MovementTrackingRoutine()
        {
            while (true)
            {
                /*while (_isJumping)
                    yield return null;*/

                /*_animatorController.TriggerAnimation("Walk");*/

                direction = ControllerHelper.PrepareDirection(obj.ReadValue<Vector2>());
                /*_animatorController.FlipSprite(obj.ReadValue<Vector2>());*/

                yield return null;
            }
        }
    }

    protected virtual void MoveCanceled(InputAction.CallbackContext obj)
    {
        /*if (_isJumping)
            return;*/

        if (c_movement == null)
            return;

        StopCoroutine(c_movement);
        c_movement = null;
        direction = ControllerHelper.PrepareDirection(Vector2.zero);

        /*_animatorController.TriggerAnimation("Walk", false);
        _animatorController.TriggerAnimation("Idle");*/
    }

    protected void JumpInput(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        /*rb.AddForce(Vector2.up * jump_velocity, ForceMode2D.Impulse);*/
        rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
    }
}
