using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_So dialogueEmpty;
    public DialogueData_So dialogueFinished;

    private bool _isTalking;

    private Stack<string> _dialogueEmptyStack;
    private Stack<string> _dialogueFinishedStack;

    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        _dialogueEmptyStack = new Stack<string>();
        _dialogueFinishedStack = new Stack<string>();
        // 倒序放入栈对话
        for (int i = dialogueEmpty.dialogueList.Count - 1; i > -1 ; i--)
        {
             _dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinished.dialogueList.Count - 1; i > -1 ; i--)
        {
            _dialogueFinishedStack.Push(dialogueFinished.dialogueList[i]);
             
        }
    }


    public void showDialogueEmpty()
    {
        if (!_isTalking)
            StartCoroutine(DialogueRoutine(_dialogueEmptyStack));
    }
    
    public void showDialogueFinished()
    {
        if (!_isTalking)
            StartCoroutine(DialogueRoutine(_dialogueFinishedStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        _isTalking = true;
        if (data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            _isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.Pause);
        }
        else
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            _isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.GamePlay);

        }
    }
    
}
