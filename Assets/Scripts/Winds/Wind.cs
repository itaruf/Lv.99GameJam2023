using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public GameObject direction;
    public Vector2 force;

    public Delegate<GameObject>.D2 onEntityEnter;
    public Delegate<GameObject>.D2 onEntityExit;

    Coroutine c_push;

    void Awake()
    {
        if (!direction)
        {
            direction = transform.GetChild(0).gameObject;
        }

        onEntityEnter += StartPushEntity;
        onEntityExit += StopPushEntity;

        ManagerHelper.GetGameManager().onPlayerEnterWind += StopPushEntity;
    }

    public void StartPushEntity(GameObject entity)
    {
        if (c_push != null)
            return;

        if (PlayerHelper.IsPlayer(entity))
        {          
            c_push = StartCoroutine(PushTrackingRoutine());
            IEnumerator PushTrackingRoutine()
            {
                while (true)
                {                  
                    EntityHelper.SetGravity(entity, PlayerHelper.GetPlayerData().PLAYER_GRAVITY_IN_WIND);
                    EntityHelper.GetEntityRigidbody(entity).AddForce(EntityHelper.GetForwardDirection(direction) * force * EntityHelper.GetSpeed(entity) * Time.fixedDeltaTime);

                    yield return null;
                }
            }
        }
    }

    public void StopPushEntity(GameObject entity)
    {
        if (c_push == null)
            return;

        if (PlayerHelper.IsPlayer(entity))
        {
            PlayerController player_controller = PlayerHelper.GetPlayerController();
            
            // Player is in another wind
            if (player_controller.GetIsInWind())
            {
                // We stop this wind OnExit
                StopCoroutine(c_push);
                c_push = null;
                return;
            }

            // Player isn't in a wind anymore
            StopCoroutine(c_push);
            c_push = null;
            ManagerHelper.GetGameManager().currentWind = null;
            player_controller.SetIsInWind(false);

            //WindPulse(entity);
            EntityHelper.SetGravity(entity, PlayerHelper.GetPlayerData().PLAYER_GRAVITY);
        }
    }
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject entity = collision.gameObject;
        if (entity == PlayerHelper.GetPlayer().gameObject)
        {
            ManagerHelper.GetGameManager().currentWind = this;
            onEntityEnter?.Invoke(entity);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject entity = collision.gameObject;
        if (entity == PlayerHelper.GetPlayer().gameObject)
        {
            PlayerController player_controller = PlayerHelper.GetPlayerController();
            player_controller.SetIsInWind(false);
            onEntityExit?.Invoke(entity);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject entity = collision.gameObject;
        if (entity == PlayerHelper.GetPlayer().gameObject)
        {
            PlayerController player_controller = PlayerHelper.GetPlayerController();
            player_controller.SetIsInWind(true);
        }
    }

    void WindPulse(GameObject entity)
    {
        Debug.Log("pulse");

        EntityHelper.GetEntityRigidbody(entity).AddForce(EntityHelper.GetForwardDirection(direction) * 10000 * EntityHelper.GetSpeed(entity) * Time.fixedDeltaTime);
    }
}