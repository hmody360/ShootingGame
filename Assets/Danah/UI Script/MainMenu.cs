using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Pages")]
    public GameObject mainMenuPage;
    public GameObject settingsPage;
    [Header("HUD")]
    public GameObject HUD;
    public GameObject hudBase; 
    public GameObject hudWeapon;

    [Header("Panle")]
    public GameObject collectiblesPanel;
    public GameObject LosePanel;
    public GameObject winPanel;

    void Start()
    {
        ShowMainMenu(); 
    }


    public void ShowMainMenu()
    {
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenuPage.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        HUD.SetActive(true);
        hudBase.SetActive(true);     
        hudWeapon.SetActive(false);
        mainMenuPage.SetActive(false);

    }
    public void ToggleCollectibles()
    {
        bool state = collectiblesPanel.activeSelf;
        collectiblesPanel.SetActive(!state);
    }
    public void LoseScreen()
    {
        HUD.SetActive(false);
        LosePanel.SetActive(true);
    }
    public void winScreen()
    {
        HUD.SetActive(false);
        winPanel.SetActive(true);
    }

}
