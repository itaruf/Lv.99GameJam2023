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

        speed = playerData.PLAYER_SPEED;

        rb = PlayerHelper.GetPlayerRigidBody();
        EntityHelper.SetGravity(gameObject, playerData.PLAYER_GRAVITY);
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
                rb.AddForce(EntityHelper.GetForwardDirection(gameObject) * ManagerHelper.GetWindManager().windForce * speed * Time.fixedDeltaTime);
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
}
