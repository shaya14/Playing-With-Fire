using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer _spriteRendererStart;
    public AnimatedSpriteRenderer _spriteRendererMiddle;
    public AnimatedSpriteRenderer _spriteRendererEnd;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        _spriteRendererStart.enabled = renderer == _spriteRendererStart;
        _spriteRendererMiddle.enabled = renderer == _spriteRendererMiddle;
        _spriteRendererEnd.enabled = renderer == _spriteRendererEnd;

        renderer.enabled = true;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float time)
    {
        Destroy(gameObject, time);
    }
}
