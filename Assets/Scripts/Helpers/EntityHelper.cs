using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityHelper
{
    public static Vector2 GetPosition(GameObject entity)
    {
        return entity.transform.position;
    }

    public static Quaternion GetRotation(GameObject entity)
    {
        return entity.transform.rotation;
    }

    public static Vector2 GetForwardDirection(GameObject entity)
    {
        return GetRotation(entity) * Vector2.up;
    }

    public static Vector2 GetBackwardDirection(GameObject entity)
    {
        return GetRotation(entity) * Vector2.down;
    }

    public static Vector2 GetRightDirection(GameObject entity)
    {
        return GetRotation(entity) * Vector2.right;
    }

    public static Vector2 GetLeftDirection(GameObject entity)
    {
        return GetRotation(entity) * Vector2.left;
    }

    public static Rigidbody2D GetEntityRigidbody(GameObject entity)
    {
        entity.TryGetComponent(out Rigidbody2D rb);
        return rb;
    }

    public static void SetGravity(GameObject entity, float newValue)
    {
        GetEntityRigidbody(entity).gravityScale = newValue;
    }

    public static Controller GetController(GameObject entity)
    {
        entity.TryGetComponent(out Controller c);
        return c;
    }

    public static Vector2 GetSpeed(GameObject entity)
    {
        return GetController(entity).GetSpeed();
    }

    public static BoxCollider2D GetBoxCollider2D(GameObject entity)
    {
        entity.TryGetComponent(out BoxCollider2D boxCollider2D);
        return boxCollider2D;
    }
}
