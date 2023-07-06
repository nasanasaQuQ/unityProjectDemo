using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class H2AReset : InterActive
{
    private Transform _gearSprite;

    private void Awake()
    {
        _gearSprite = transform.GetChild(0);
    }

    public override void EmptyClick()
    {
        // 重置游戏
        GameController.Instance.ResetGame();
        _gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
    }
}
