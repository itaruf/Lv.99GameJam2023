using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : Controller
{
    public D_Player D_Player;

    private bool isInWind = false;

    Coroutine c_moveContinously;
    Coroutine c_flicker;

    void Awake()
    {
        base.Awake();

        if (D_Player.player_min_speed.x > D_Player.player_speed.x)
            D_Player.player_min_speed.x = D_Player.player_speed.x;

        if (D_Player.player_min_speed.y > D_Player.player_speed.y)
            D_Player.player_min_speed.y = D_Player.player_speed.y;

        speed = D_Player.player_speed;
        base_speed = speed;

        rb = PlayerHelper.GetPlayerRigidBody();
        EntityHelper.SetGravity(gameObject, D_Player.player_gravity);


    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ModifySpeed(base_speed, true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ModifySpeed(D_Player.player_max_speed, true);
        }
    }*/

    void Start()
    {
        MoveContinuously();   
    }

    public bool GetIsInWind()
    {
        return isInWind;
    }

    public void SetIsInWind(bool newBool)
    {
        isInWind = newBool;
    }

    public void MoveContinuously()
    {
        c_moveContinously = StartCoroutine(MoveContinuouslyRoutine());
        IEnumerator MoveContinuouslyRoutine()
        {
            while (true)
            {

                rb.AddForce(EntityHelper.GetForwardDirection(gameObject) * ManagerHelper.GetGlobalWindForce() * speed);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void StopMoveContinuously()
    {
        if (c_moveContinously == null)
            return;

        StopCoroutine(c_moveContinously);
        c_moveContinously = null;
    }

    public void ModifySpeed(Vector2 newVector2, bool shouldOverride = false)
    {
        Vector2 v;
        float x, y;

        if (!shouldOverride)
        {
            x = (speed + newVector2).x;
            y = (speed + newVector2).y;
        }
        
        else
        {
            x = newVector2.x;
            y = newVector2.y;
        }

        v.x = Mathf.Clamp(x, PlayerHelper.GetPlayerData().player_min_speed.x, PlayerHelper.GetPlayerData().player_max_speed.x);
        v.y = Mathf.Clamp(y, PlayerHelper.GetPlayerData().player_min_speed.y, PlayerHelper.GetPlayerData().player_max_speed.y);

        speed = v;

        // Super Boost
        if (speed.x > base_speed.x && speed.y > base_speed.y)
        {
            if (!isSuperBoostOn)
            {
                isSuperBoostOn = true;
                onStartSuperSpeed();
            }
        }
        else
        {
            if (isSuperBoostOn)
            {
                isSuperBoostOn = false;
                onStopSuperSpeed();
            }
        }
    }

    public void StartFlickerOnHit()
    {
        if (c_flicker != null)
            return;

        float current_time = 1.5f;

        c_flicker = StartCoroutine(FlickerTracking());
        IEnumerator FlickerTracking()
        {
           SpriteRenderer player_sprite = EntityHelper.GetSpriteRenderer(gameObject);
            while (current_time > 0)
            {
                current_time -= 0.5f;
                player_sprite.enabled = !player_sprite.enabled;

                yield return new WaitForSeconds(0.15f);
            }

            player_sprite.enabled = true;
            StopFlickerOnHit();
        }
    }

    public void StopFlickerOnHit()
    {
        if (c_flicker != null)
        {
            StopCoroutine(c_flicker);
            c_flicker = null;
        }
    }
}
