using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Spawner;

public class Wind : MonoBehaviour, ISpawnable
{
    [SerializeField] WindData windData;
    public GameObject direction;
    
    Vector2 force;

    public Delegate.D2 onEntityEnter;
    public Delegate.D2 onEntityExit;

    ISpawnable iSpawnable;

    Coroutine c_push;
    Coroutine c_delete;

    void Awake()
    {
        if (windData)
            force = windData.wind_force;

        if (!direction)
        {
            direction = transform.GetChild(0).gameObject;
        }

        onEntityEnter += StartPushEntity;
        onEntityExit += StopPushEntity;

        iSpawnable = this as ISpawnable;

        ManagerHelper.GetSeasonManager().onSeasonChange += (ESeasons eason) => { iSpawnable.DeleteEntity(); };

        iSpawnable.DeleteEntityBelowPlayer();
    }

    private void OnDestroy()
    {
        onEntityEnter -= StartPushEntity;
        onEntityExit -= StopPushEntity;

        if (c_delete != null)
        {
            StopCoroutine(c_delete);
            c_delete = null;
        }

        if (c_push != null)
        {
            StopCoroutine(c_push);
            c_push = null;
        }
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
                    EntityHelper.SetGravity(entity, PlayerHelper.GetPlayerData().player_gravity_in_wind);
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
            ManagerHelper.GetWindManager().currentWind = null;
            player_controller.SetIsInWind(false);

            //WindPulse(entity);
            EntityHelper.SetGravity(entity, PlayerHelper.GetPlayerData().player_gravity);
        }
    }
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject entity = collision.gameObject;
        if (entity == PlayerHelper.GetPlayer().gameObject)
        {
            ManagerHelper.GetWindManager().currentWind = this;
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

    void ISpawnable.DeleteEntityBelowPlayer()
    {
        if (c_delete != null)
            return;

        c_delete = StartCoroutine(DeleteEntityBelowPlayerTracking());
        IEnumerator DeleteEntityBelowPlayerTracking()
        {
            while (true)
            {
                if (MathsHelper.CompareFloat(EntityHelper.GetPosition(gameObject).y, PlayerHelper.GetPlayerPosition().y, MathsHelper.EMathSymbol.LOWER))
                {
                    Destroy(gameObject);
                }
                yield return null;
            }
        }
    }

    void ISpawnable.DeleteEntity()
    {
        if (this)
            Destroy(gameObject);
    }
}
