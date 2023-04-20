using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : Controller
{
    public PlayerData playerData;

    private bool isInWind = false;

    Coroutine c_moveContinously;

    void Awake()
    {
        base.Awake();

        if (playerData.player_min_speed.x > playerData.player_speed.x)
            playerData.player_min_speed.x = playerData.player_speed.x;

        if (playerData.player_min_speed.y > playerData.player_speed.y)
            playerData.player_min_speed.y = playerData.player_speed.y;

        speed = playerData.player_speed;
        base_speed = speed;

        rb = PlayerHelper.GetPlayerRigidBody();
        EntityHelper.SetGravity(gameObject, playerData.player_gravity);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ModifySpeed(base_speed, true);
        }
    }
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
                rb.AddForce(EntityHelper.GetForwardDirection(gameObject) * ManagerHelper.GetGlobalWindForce() * speed * Time.deltaTime);
                yield return null;
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
}
