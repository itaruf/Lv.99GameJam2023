using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public D_Camera d_Camera;

    Vector2 camera_speed = new Vector2(1f, 1f);
    bool enable_camera_lag_speed = false;
    Vector2 camera_lag_speed = new Vector2(1f, 1f);
    GameObject camera_target = null;
    Vector2 camera_offset_from_target = new Vector2(0f, 0f);
    Coroutine c_follow;

    void Awake()
    {
        if (d_Camera)
        {
            enable_camera_lag_speed = d_Camera.enable_camera_lag_speed;
            camera_speed = d_Camera.camera_speed;
            camera_target = d_Camera.camera_target;
            camera_offset_from_target = d_Camera.camera_offset_from_target;
        }

        StartFollowTarget(camera_target);
    }

    void StartFollowTarget(GameObject target)
    {
        if (!target)
            return;

        c_follow = StartCoroutine(FollowTrackingRoutine());
        IEnumerator FollowTrackingRoutine()
        {
            while (true)
            {
                float x = PlayerHelper.GetPlayerPosition().x + camera_offset_from_target.x;
                float y = PlayerHelper.GetPlayerPosition().y + camera_offset_from_target.y;
                float z = transform.position.z;

                if (enable_camera_lag_speed)
                {
                    transform.position = Vector3.Slerp(transform.position, new Vector3(x, y, z), camera_speed.y * PlayerHelper.GetPlayerSpeed().y / 2 * Time.fixedDeltaTime);
                }
                else
                {
                    transform.position = new Vector3(x, y, z);
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }

    void StopFollowTarget(GameObject target)
    {

    }
}
