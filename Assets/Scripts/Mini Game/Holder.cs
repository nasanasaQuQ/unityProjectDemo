using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : InterActive
{
    public bool isEmpty;

    public HashSet<Holder> linkHolders = new HashSet<Holder>();

    public BallName matchBall;

    private Ball currentBall;

    public void CheckMatch(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyClick()
    {
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                // 移动球
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);
                
                // 交换球
                holder.CheckMatch(currentBall);
                this.currentBall = null;
                
                // 改变状态
                this.isEmpty = true;
                holder.isEmpty = false;
                
                EventHandler.CallCheckGameStateEvent();
            }
        }
    }
}
