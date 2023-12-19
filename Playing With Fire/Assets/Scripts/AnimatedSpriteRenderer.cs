using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite _idleSprite;
    public Sprite[] _animationSprites;
    public float _animationTime = 0.25f;
    private int _animationFrame;
    public bool _loop = true;
    public bool _idle = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), _animationTime, _animationTime);
    }

    private void NextFrame()
    {
        _animationFrame++;
        if(_loop && _animationFrame >= _animationSprites.Length)
        {
            _animationFrame = 0;
        }

        if(_idle)
        {
            _spriteRenderer.sprite = _idleSprite;
        }
        else if(_animationFrame >= 0 && _animationFrame < _animationSprites.Length)
        {
            _spriteRenderer.sprite = _animationSprites[_animationFrame];
        }
    }
}
