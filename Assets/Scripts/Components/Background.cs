using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour, IActivity
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Awake()
    {
        if (!spriteRenderer)
        {
            TryGetComponent(out spriteRenderer);
        }

        if (!animator)
        {
            TryGetComponent(out animator);
        }
    }

    void IActivity.Activation()
    {
        EntityHelper.SetActive(spriteRenderer, true);
        EntityHelper.SetActive(animator, true);
    }

    void IActivity.Deactivation()
    {
        EntityHelper.SetActive(spriteRenderer, false);
        EntityHelper.SetActive(animator, false);
    }

}
