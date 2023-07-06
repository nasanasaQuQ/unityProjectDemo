using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameH2A_SO gameData;

    public Transform[] holderTransforms;

    public LineRenderer linePrefab;

    public Ball ballPrefab;

    public GameObject lineParent;


    private void Start()
    {
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
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }
}
