using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public void Collect();

    public void StartFollowPlayer();
    public void StopFollowPlayer();

    public float GetFollowDuration();
    public void SetFollowDuration(float newValue);

    public Vector2 GetSpeed();
    public void SetSpeed(Vector2 newValue);

    public int GetScoreGiven();
    public void SetScoreGiven(int newValue);

    public Vector2 GetSpeedGiven();
    public void SetSpeedGiven(Vector2 newValue);

}
