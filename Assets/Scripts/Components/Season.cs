using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Season : MonoBehaviour, IActivity
{
    Coroutine c_active;
    IActivity iActivity;

    void Awake()
    {
        iActivity = this as IActivity;
    }

    void IActivity.Activation()
    {
        if (c_active != null)
            return;

        Player player = PlayerHelper.GetPlayer();

        c_active = StartCoroutine(ActiveTracking());
        IEnumerator ActiveTracking()
        {
            while (true)
            {
                float y_upper_bound = EntityHelper.GetBoxCollider2D(gameObject).bounds.max.y;
                float y_lower_bound = EntityHelper.GetBoxCollider2D(gameObject).bounds.min.y;

                if (MathsHelper.CompareFloat(y_upper_bound, PlayerHelper.GetPlayerPosition().y, MathsHelper.EMathSymbol.LOWER_EQUAL))
                {
                    SeasonManager season_manager = ManagerHelper.GetSeasonManager();
                    BackgroundManager background_manager = ManagerHelper.GetBackgroundManager();
                    
                    season_manager.onSeasonChangeIndex(season_manager.GetNextSeasonIndex());
                }

                yield return null;
            }
        }
    }

    void IActivity.Deactivation()
    {
        if (c_active != null)
        {
            StopCoroutine(c_active);
            c_active = null;
        }
    }
}
