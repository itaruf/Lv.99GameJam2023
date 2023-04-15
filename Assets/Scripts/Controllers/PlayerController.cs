using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public PlayerData playerData;

    private bool isInWind = false;

    void Awake()
    {
        base.Awake();

        speed = playerData.PLAYER_SPEED;

        rb = PlayerHelper.GetPlayerRigidBody();
        EntityHelper.SetGravity(gameObject, playerData.PLAYER_GRAVITY);
    }

    public bool GetIsInWind()
    {
        return isInWind;
    }

    public void SetIsInWind(bool newBool)
    {
        isInWind = newBool;
    }

}
