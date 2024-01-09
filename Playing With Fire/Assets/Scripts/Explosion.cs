using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererStart;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererMiddle;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererEnd;

    // CR: [discuss] these don't need to be public.
    //     You are only using them as parameters to your 'Init' function.
    //     but - the caller of your 'Init' function shouldn't be exposed to the explosion's
    //     details such as sprites. See also code section below.
    public AnimatedSpriteRenderer spriteRendererStart => _spriteRendererStart;
    public AnimatedSpriteRenderer spriteRendererMiddle => _spriteRendererMiddle;
    public AnimatedSpriteRenderer spriteRendererEnd => _spriteRendererEnd;

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
    public void SetRendererAndDuration(AnimatedSpriteRenderer renderer, float time)
    {
        SetActiveRenderer(renderer);
        DestroyAfter(time);
    }

    // CR: rename Init.
    // CR: 'time' should be a SerializeField, not a parameter (explsionTime is always the same for)
    //     all explosions and does not change during the game.
    // CR: [discuss] see below:
            // public enum ExplosionAppearance {
            //     Start, // i.e. the explosion's center
            //     Middle,
            //     End
            // }
            // public void Init(ExplosionAppearance appearance, Vector2 direction)
    public void SetRendererDirectionAndDuration(AnimatedSpriteRenderer renderer, Vector2 direction, float time)
    {
        SetActiveRenderer(renderer);
        SetDirection(direction);
        DestroyAfter(time);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Damageable>().TakeDamage(1);
        }
    }
}
