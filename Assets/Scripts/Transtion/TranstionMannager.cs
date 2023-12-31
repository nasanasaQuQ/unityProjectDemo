using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranstionMannager : Singleton<TranstionMannager>, ISaveable
{

    [SceneName] public string startScene;
    
    
    public CanvasGroup fadeCanvansGroup;
    public float fadeDuration;
    private bool isFade;

    private bool _canTransition;
    private void Start()
    {
        //StartCoroutine(TransitionToSence(string.Empty, startScene));
        ISaveable saveable = this;
        saveable.SaveableRegister();
        
    }

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        _canTransition = gameState == GameState.GamePlay;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToSence("menu", startScene));
    }

    public void Transition(string from, string togo)
    {
        if (!isFade && _canTransition)
            StartCoroutine(TransitionToSence(from, togo));
        
    }
    
    // 异步加载卸载场景
    private IEnumerator TransitionToSence(string from, string togo)
    {
        yield return Fade(1);
        if (from != string.Empty){
        // 切换场景时判断物品
            EventHandler.CallBeforeSceneUnloadEvent();
            
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(togo, LoadSceneMode.Additive);

        
        Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(scene);
        
        
        EventHandler.CallAfterSceneUnloadEvent();
        yield return Fade(0);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targerAlpha">1黑0透</param>
    /// <returns></returns>
    private IEnumerable Fade(float targerAlpha)
    {
        isFade = true;

        fadeCanvansGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(fadeCanvansGroup.alpha - targerAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvansGroup.alpha, targerAlpha))
        {
            fadeCanvansGroup.alpha = Mathf.MoveTowards(fadeCanvansGroup.alpha, targerAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fadeCanvansGroup.blocksRaycasts = false;

        isFade = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.CurrentScene = SceneManager.GetActiveScene().name;
        return gameSaveData;
    }

    public void RestoreGameData(GameSaveData gameSaveData)
    {
        Transition("menu", gameSaveData.CurrentScene);
    }
}
