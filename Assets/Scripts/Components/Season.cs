using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Season : MonoBehaviour, IActivity
{
    public List<Background> backgrounds;

    Coroutine c_active;
    IActivity iActivity;

    void Awake()
    {
        Background[] backgrounds_array = GetComponentsInChildren<Background>();

        if (backgrounds.Contains(null))
            backgrounds.Clear();

        if (backgrounds.Count != backgrounds_array.Length)
        {
            backgrounds.Clear();
            foreach(Background background in backgrounds_array)
            {
                backgrounds.Add(background);
            }
        }

        iActivity = this as IActivity;
    }

    void Deactivation()
    {
        if (c_active != null)
        {
            StopCoroutine(c_active);
            c_active = null;
        }
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
                    season_manager.onSeasonChange(background_manager.season_map.ElementAt(season_manager.GetNextSeasonIndex()).Key);

                    PlayerHelper.SetPlayerPosition(new Vector3(PlayerHelper.GetPlayerPosition().x, y_lower_bound, 0));
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
