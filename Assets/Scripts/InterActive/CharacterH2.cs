using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterH2 : InterActive
{
    private DialogueController _dialogueController;

    private void Awake()
    {
        _dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyClick()
    {
        if (isDone)
            _dialogueController.showDialogueFinished();
        else
            _dialogueController.showDialogueEmpty();
    }

    protected override void OnClickedAction()
    {
        _dialogueController.showDialogueFinished();
    }
}
