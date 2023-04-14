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
}
