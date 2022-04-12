using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerMenu : MonoBehaviour
{

    [Header("Panels")]
    public GameObject Settings;

    public void Starten()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name); //Homescreen
        SceneManager.LoadScene("MapOverlay"); // MapOverlay
        
    }
    public void Zurücksetzen()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SettingsStarten()
    {
        Settings.SetActive(true);
        Time.timeScale = 0;
    }

    public void SettingsSchließen()
    {
        Settings.SetActive(false);
        Time.timeScale = 1;
    }

    public void SpielVerlassen()
    {
        Application.Quit();
    }
}
