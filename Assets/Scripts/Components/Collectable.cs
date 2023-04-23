using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Entity, ICollectable, ISpawnable
{
    [SerializeField] D_Collectable collectableData;

    GameObject collectable_entity;
    int collectable_score_given;
    Vector2 collectable_speed_given;
    Vector2 collectable_speed;
    float collectable_follow_duration;

    ISpawnable iSpawnable;
    ICollectable iCollectable;

    Coroutine c_follow;
    Coroutine c_delete;

    public AudioSource audio;

    void Awake()
    {
        if (collectableData)
        {
            collectable_entity = collectableData.entity;
            collectable_score_given = collectableData.collectable_score_given;
            collectable_speed_given = collectableData.collectable_speed_given;
            collectable_speed = collectableData.collectable_speed;
            collectable_follow_duration = collectableData.collectable_follow_duration;
        }

        iSpawnable = this as ISpawnable;
        iCollectable = this as ICollectable;

        ManagerHelper.GetSeasonManager().onSeasonChangeIndex += (int index) => { iSpawnable.DeleteEntity(); };

        iSpawnable.DeleteEntityBelowPlayer();

        if (!audio)
            TryGetComponent(out audio);
    }

    private void OnDestroy()
    {
        if (c_delete != null)
        {
            StopCoroutine(c_delete);
            c_delete = null;
        }

        if (c_follow != null)
        {
            StopCoroutine(c_follow);
            c_follow = null;
        }
    }

    void ICollectable.Collect()
    {
        EntityHelper.SetActiveCollider2D(gameObject, false);
        audio.Play();

        if (collectableData)
            iCollectable.StartFollowPlayer();
    }

    void ICollectable.StartFollowPlayer()
    {
        float current_time = collectable_follow_duration;

        float x_offset = Random.Range(-0.5f, 0.5f);
        float y_offset = Random.Range(0.5f, 1.5f);

        c_follow = StartCoroutine(FollowTrackingRoutine());
        IEnumerator FollowTrackingRoutine()
        {
            while (current_time > 0)
            {
                current_time -= Time.deltaTime;
                transform.position = Vector3.Slerp(transform.position, new Vector3(PlayerHelper.GetPlayerPosition().x + x_offset, PlayerHelper.GetPlayerPosition().y - y_offset), PlayerHelper.GetPlayerSpeed().x * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
            }

            (this as ICollectable).StopFollowPlayer();
        }
    }

    void ICollectable.StopFollowPlayer()
    {
        if (c_follow != null)
        {
            StopCoroutine(c_follow);
            c_follow = null;
        }

        Destroy(gameObject);
    }
    
    float ICollectable.GetFollowDuration()
    {
        return collectable_follow_duration;
    }

    void ICollectable.SetFollowDuration(float newValue)
    {
        collectable_follow_duration = newValue;
    }

    Vector2 ICollectable.GetSpeed()
    {
        return collectable_speed;
    }

    void ICollectable.SetSpeed(Vector2 newValue)
    {
        collectable_speed = newValue;
    }

    int ICollectable.GetScoreGiven()
    {
        return collectable_score_given;
    }

    void ICollectable.SetScoreGiven(int newValue)
    {
        collectable_score_given = newValue;
    }

    Vector2 ICollectable.GetSpeedGiven()
    {
        return collectable_speed_given;
    }

    void ICollectable.SetSpeedGiven(Vector2 newValue)
    {
        throw new System.NotImplementedException();
    }

    void ISpawnable.DeleteEntityBelowPlayer()
    {
        return;

        if (c_delete != null)
            return;

        c_delete = StartCoroutine(DeleteEntityBelowPlayerTracking());
        IEnumerator DeleteEntityBelowPlayerTracking()
        {
            while (true)
            {
                if (MathsHelper.CompareFloat(EntityHelper.GetPosition(gameObject).y, PlayerHelper.GetPlayerPosition().y, MathsHelper.EMathSymbol.LOWER))
                {
                    if (c_follow == null)
                    {
                        Destroy(gameObject);
                    }
                    else
                        yield break;
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
