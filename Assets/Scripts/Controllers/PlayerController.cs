using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    void Awake()
    {
        base.Awake();

        rb = PlayerHelper.GetPlayerRigidBody();
    }
}
