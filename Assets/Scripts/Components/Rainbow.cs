using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour, IActivity
{
    [SerializeField] public GameObject owner;

    SpriteRenderer spriteRenderer;
    Animator animator;

    public AudioSource audio;

    void Awake()
    {
        IActivity iactivity = (this as IActivity);
        
        if (!spriteRenderer)
        {
            TryGetComponent(out spriteRenderer);
        }

        if (!animator)
        {
            TryGetComponent(out animator);
        }

        if (!owner)
        {
            owner = transform.parent.gameObject;

            if (owner.TryGetComponent(out PlayerController c))
            {

                c.onStartSuperSpeed += iactivity.Activation;
                c.onStopSuperSpeed += iactivity.Deactivation;
            }
        }

        if (!audio)
            TryGetComponent(out audio);

        iactivity.Deactivation();
    }

    void IActivity.Activation()
    {
        EntityHelper.SetActive(spriteRenderer, true);
        EntityHelper.SetActive(animator, true);
        audio.Play();
    }

    void IActivity.Deactivation()
    {
        EntityHelper.SetActive(spriteRenderer, false);
        EntityHelper.SetActive(animator, false);
        audio.Stop();
    }
}
