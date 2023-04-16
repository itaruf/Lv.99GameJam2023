using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public CircleCollider2D collectCollider;

    void Awake()
    {
        if (!collectCollider)
            TryGetComponent(out collectCollider);

        ScoreManager scoreManager = ManagerHelper.GetScoreManager();
        if (scoreManager)
        {
            /*scoreManager.onPlayerScore += () => { PlayerHelper.GetPlayerController().ModifySpeed(new Vector2(1, 1)); }; */
            scoreManager.onScoreChange += scoreManager.SetScore;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collectable collectable))
        {
            (collectable as ICollectable).Collect();

            ScoreManager scoreManager = ManagerHelper.GetScoreManager();
            if (scoreManager)
            {
                /*scoreManager.onPlayerScore?.Invoke();*/
                scoreManager.onScoreChange?.Invoke(collectable.score_given);
                PlayerHelper.GetPlayerController().ModifySpeed(new Vector2(collectable.speed_given, collectable.speed_given));
            }
        }
    }
}
