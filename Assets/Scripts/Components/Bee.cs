using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spawner;

public class Bee : MonoBehaviour, ISpawnable, IActivity
{
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] public D_Bee d_Bee;

    Vector2 bee_speed;
    Vector2 bee_speed_loss_given = new Vector2(1f, 1f);
    Vector2 bee_direction;

    ISpawnable iSpawnable;
    IActivity iActivity;

    Coroutine c_move;

    void Awake()
    {
        if (!sprite_renderer)
            TryGetComponent(out sprite_renderer);

        if (!animator)
            TryGetComponent(out animator);

        if (!rb)
            TryGetComponent(out rb);

        if (d_Bee)
        {
            bee_speed = d_Bee.bee_speed;
            bee_speed_loss_given = d_Bee.bee_speed_loss_given;
        }

        iSpawnable = this as ISpawnable;
        iActivity = this as IActivity;

        if (MathsHelper.CompareFloat(EntityHelper.GetPosition(gameObject).x, PlayerHelper.GetPlayerPosition().x, MathsHelper.EMathSymbol.HIGHER))
        {
            bee_direction = Vector2.left;
            sprite_renderer.flipX = true;
        }
        else
            bee_direction = Vector2.right;

        ManagerHelper.GetSeasonManager().onSeasonChangeIndex += (int index) => { iSpawnable.DeleteEntity(); };
    }

    void Start()
    {
        iActivity.Activation();
    }

    private void OnDestroy()
    {
        if (c_move != null)
        {
            StopCoroutine(c_move);
            c_move = null;
        }
    }

    void ISpawnable.DeleteEntity()
    {
        if (this)
            Destroy(gameObject);
    }

    void ISpawnable.DeleteEntityBelowPlayer()
    {

    }

    void SetDirection()
    {
        if (bee_direction == Vector2.left)
            bee_direction = Vector2.right;
        else
            bee_direction = Vector2.left;

        sprite_renderer.flipX = !sprite_renderer.flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            rb.velocity = new Vector2(0, 0);
            SetDirection();
        }

        if (PlayerHelper.IsPlayer(collision.gameObject))
        {
            Vector2 player_speed = PlayerHelper.GetPlayerSpeed();
            PlayerHelper.GetPlayerController().ModifySpeed(
                new Vector2
                (
                    player_speed.x - bee_speed_loss_given.x,
                    player_speed.y - bee_speed_loss_given.y
                ), true
            );

            /*iSpawnable.DeleteEntity();*/
        }
    }

    void IActivity.Activation()
    {
        if (c_move != null)
            return;

        c_move = StartCoroutine(MoveTracking());
        IEnumerator MoveTracking()
        {
            while (true)
            {
                rb.AddForce(bee_direction * bee_speed);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void IActivity.Deactivation()
    {

    }
}
