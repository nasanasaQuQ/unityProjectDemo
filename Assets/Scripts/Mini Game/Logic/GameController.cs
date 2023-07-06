using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent onFinish;
    
    [Header("游戏数据")]
    public GameH2A_SO gameData;

    public Transform[] holderTransforms;

    public LineRenderer linePrefab;

    public Ball ballPrefab;

    public GameObject lineParent;

    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }
    
    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }

    private void Start()
    {
        DrawLines();
        CreateBall();
    }
    
    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
                return;
        }

        foreach (var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        EventHandler.CallGamePassEvent(gameData.gameName);
        onFinish?.Invoke();
    }

    public void ResetGame()
    {
        for (int i = 0; i < lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }

        foreach (var holder in holderTransforms)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }
        
        DrawLines();
        CreateBall();
    }

    public void DrawLines()
    {
        foreach (var connection in gameData.lineConnections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0,holderTransforms[connection.from].position);
            line.SetPosition(1,holderTransforms[connection.to].position);
            
            // 创建每个holder的链接关系
            holderTransforms[connection.from].GetComponent<Holder>().linkHolders
                .Add(holderTransforms[connection.to].GetComponent<Holder>());
            holderTransforms[connection.to].GetComponent<Holder>().linkHolders
                .Add(holderTransforms[connection.from].GetComponent<Holder>());
        }
    }

    public void CreateBall()
    {
        for (var i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] == BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }

            var ball = Instantiate(ballPrefab, holderTransforms[i]);
            
            holderTransforms[i].GetComponent<Holder>().CheckMatch(ball);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }
}
