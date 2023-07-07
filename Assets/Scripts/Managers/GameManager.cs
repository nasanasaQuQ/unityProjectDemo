using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Dictionary<string, bool> _miniGameDictionary = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;

    }

    private void OnDisable()
    {
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        
    }

    private void OnGamePassEvent(string gameName)
    {
        _miniGameDictionary[gameName] = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnAfterSceneUnloadEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (_miniGameDictionary.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("menu",LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
    }
    
    
    
}
