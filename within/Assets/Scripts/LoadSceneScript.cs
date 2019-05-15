using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    public string sceneName;
    
    public void GameStart()
    {
        Application.LoadLevel(sceneName);
    }
}
