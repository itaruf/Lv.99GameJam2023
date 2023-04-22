using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Props : MonoBehaviour, ISpawnable, IActivity
{
    [SerializeField] public float rotation_speed = 8f;
    [SerializeField] SpriteRenderer spriteRenderer;
    ISpawnable iSpawnable;
    IActivity iActivity;

    Coroutine c_rotate;
    
    void Awake()
    {
        if (!spriteRenderer)
            TryGetComponent(out spriteRenderer);

        iSpawnable = this as ISpawnable;
        iActivity = this as IActivity;

        ManagerHelper.GetSeasonManager().onSeasonChangeIndex += (int index) => { iSpawnable.DeleteEntity(); };
    }

    void Start()
    {
        iActivity.Activation();
    }

    private void OnDestroy()
    {
        if (c_rotate != null)
        {
            StopCoroutine(c_rotate);
            c_rotate = null;
        }
    }

    void IActivity.Activation()
    {
        if (c_rotate != null)
            return;

        int binary = Random.Range(0, 2);

        Vector2 axis = EntityHelper.GetLeftDirection(gameObject);
        if (binary == 0)
            axis = EntityHelper.GetRightDirection(gameObject);

        c_rotate = StartCoroutine(RotateTracking());
        IEnumerator RotateTracking()
        {
            while (true)
            {
                Vector3 direction = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, axis.x);

                transform.Rotate(new Vector3(direction.x, direction.y, direction.z * rotation_speed * Time.deltaTime));
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void IActivity.Deactivation()
    {
        if (c_rotate != null)
        {
            StopCoroutine(c_rotate);
            c_rotate = null;
        }

        Destroy(gameObject);
    }

    void ISpawnable.DeleteEntity()
    {
        if (this)
        {
            iActivity.Deactivation();
        }
    }

    void ISpawnable.DeleteEntityBelowPlayer()
    {

    }
}
