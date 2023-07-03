using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : InterActive
{

    private SpriteRenderer _spriteRenderer;

    private BoxCollider2D _boxCollider2D;

    public Sprite openbox;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
    }

    private void OnAfterSceneUnloadEvent()
    {
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            _spriteRenderer.sprite = openbox;
            _boxCollider2D.enabled = false;
        }
    }

    protected override void OnClickedAction()
    {
        _spriteRenderer.sprite = openbox;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
