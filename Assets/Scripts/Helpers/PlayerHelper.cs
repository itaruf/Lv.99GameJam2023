using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerHelper
{ 
    public static Rigidbody2D GetPlayerRigidBody()
    {
        GetPlayer().TryGetComponent(out Rigidbody2D rb);
        return rb;
    }

    public static Player GetPlayer()
    {
        return Object.FindObjectOfType<Player>();
    }

    public static bool IsPlayer(GameObject entity)
    {
        return entity == GetPlayer().gameObject;
    }

    public static PlayerController GetPlayerController()
    {
        GetPlayer().TryGetComponent(out PlayerController pc);
        return pc;
    }

    public static PlayerData GetPlayerData()
    {
        return GetPlayerController().playerData;
    }

    public static CircleCollider2D GetPlayerCollectCollider()
    {
        GetPlayer().TryGetComponent(out CircleCollider2D cc2D);
        return cc2D;
    }

    public static Vector3 GetPlayerPosition()
    {
        return GetPlayer().transform.position;
    }

    public static Vector2 GetPlayerSpeed()
    {
        return GetPlayerController().GetSpeed();
    }
}
