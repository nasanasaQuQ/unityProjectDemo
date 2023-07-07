using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectMannager : MonoBehaviour, ISaveable
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.UpdateUIEvent += UpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.UpdateUIEvent -= UpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int obj)
    {   
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void UpdateUIEvent(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnAfterSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            
                itemAvailableDict.Add(item.itemName, true);
            
            else
            
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            
        }
        
        foreach (var item in FindObjectsOfType<InterActive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            
                itemAvailableDict.Add(item.itemName, true);
            
        }

        foreach (var item in FindObjectsOfType<InterActive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.itemAvailableDict = itemAvailableDict;
        gameSaveData.interactiveStateDict = interactiveStateDict;
        return gameSaveData;
    }

    public void RestoreGameData(GameSaveData gameSaveData)
    {
        itemAvailableDict = gameSaveData.itemAvailableDict;
        interactiveStateDict = gameSaveData.interactiveStateDict;
    }
}
