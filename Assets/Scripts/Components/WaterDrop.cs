using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour, ISpawnable, IActivity
{
    [SerializeField] D_WaterDrop d_WaterDrop;

    GameObject waterdrop_entity;
    Vector2 waterdrop_speed_loss_given;

    ISpawnable iSpawnable;
    IActivity iActivity;

    void Awake()
    {
        if (d_WaterDrop)
        {
            waterdrop_entity = d_WaterDrop.entity;
            waterdrop_speed_loss_given = d_WaterDrop.waterdrop_speed_loss_given;
        }

        iSpawnable = this as ISpawnable;
        iActivity = this as IActivity;
    }

    void IActivity.Activation()
    {

    }

    void IActivity.Deactivation()
    {
        if (this)
        {
            Destroy(gameObject);
        }
    }

    void ISpawnable.DeleteEntity()
    {
        if (this)
        {
            Destroy(gameObject);
        }
    }

    void ISpawnable.DeleteEntityBelowPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerHelper.IsPlayer(collision.gameObject))
        {
            Vector2 player_speed = PlayerHelper.GetPlayerSpeed();
            PlayerHelper.GetPlayerController().ModifySpeed(
                new Vector2
                (
                    player_speed.x - waterdrop_speed_loss_given.x,
                    player_speed.y - waterdrop_speed_loss_given.y
                ),  true
                );

            iSpawnable.DeleteEntity();
        }
    }
}
