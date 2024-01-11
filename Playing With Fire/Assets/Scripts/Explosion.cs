using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum Appearance {
        Start, // i.e. the explosion's center
        Middle,
        End
    }

    [SerializeField] private float _explosionDuration;

    [SerializeField] private AnimatedSpriteRenderer _spriteRendererStart;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererMiddle;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererEnd;

    public void SetActiveRenderer(Appearance appearance)
    {
        if (appearance == Appearance.Start) {
            _spriteRendererStart.enabled = true;
        } else if (appearance == Appearance.Middle) {
            _spriteRendererMiddle.enabled = true;
        } else {
            _spriteRendererEnd.enabled = true;
        }
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
    public void SetRendererAndDuration(Appearance appearance)
    {
        SetActiveRenderer(appearance);
        DestroyAfter(_explosionDuration);
    }

    public void Init(Appearance appearance, Vector2 direction)
    {
        SetActiveRenderer(appearance);
        SetDirection(direction);
        DestroyAfter(_explosionDuration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Damageable>().TakeDamage(1);
        }
    }
}
