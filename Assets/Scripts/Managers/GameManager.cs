using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{

    private Dictionary<string, bool> _miniGameDictionary = new Dictionary<string, bool>();
    private int _gameWeek;
    private GameController _currentGame;
    private void OnEnable()
    {
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnDisable()
    {
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("menu",LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);

        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this._gameWeek = gameWeek;
        _miniGameDictionary.Clear();
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

        _currentGame = FindObjectOfType<GameController>();
        _currentGame?.SetGameWeekData(_gameWeek);
    }
    


    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.GameWeek = this._gameWeek;
        saveData.miniGameStateDict = this._miniGameDictionary;
        return saveData;
    }

    public void RestoreGameData(GameSaveData gameSaveData)
    {
        this._gameWeek = gameSaveData.GameWeek;
        this._miniGameDictionary = gameSaveData.miniGameStateDict;
    }
}
