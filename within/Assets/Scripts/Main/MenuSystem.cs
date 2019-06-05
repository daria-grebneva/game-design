using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    
    public GameObject MainMenu;
    public GameObject StartMainMenu;
    public GameSystem MainGameSystem;

    public void SaveGame()
    {
        PlayerPrefs.SetInt("LEVEL",MainGameSystem.MainLevel);
        PlayerPrefs.SetInt("POINT",MainGameSystem.PointLevel);
        PlayerPrefs.SetInt("LEVEL_Att",MainGameSystem.WinAndAtt.y);
        PlayerPrefs.SetInt("LEVEL_Win",MainGameSystem.WinAndAtt.x);
    }

    public void ClearGame()
    {
        PlayerPrefs.SetInt("LEVEL",0);
        PlayerPrefs.SetInt("POINT",0);
        PlayerPrefs.SetInt("LEVEL_Att",0);
        PlayerPrefs.SetInt("LEVEL_Win",0);
    }
    
    public void ExitApplication()
    {
        SaveGame();
        Application.Quit();
    }

    public void OpenMenu()
    {
        MainGameSystem.GamePaused = true;
        MainMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        MainGameSystem.GamePaused = false;
        MainMenu.SetActive(false);
    }
    
    public void StartGame()
    {
        StartMainMenu.SetActive(false);
        MainGameSystem.StartGame();
    }
    
    public void StartClearGame()
    {
        ClearGame();
        MainGameSystem.FirstStart = true;
        StartMainMenu.SetActive(false);
        MainMenu.SetActive(false);
        MainGameSystem.StartNewGame();
    }
}
