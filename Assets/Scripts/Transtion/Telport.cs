using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telport : MonoBehaviour
{

    [SceneName] public string sceneFrom;

    [SceneName] public string sceneTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportToSence()
    {
        TranstionMannager.Instance.Transition(sceneFrom, sceneTo);
    }
    
    
    
}
