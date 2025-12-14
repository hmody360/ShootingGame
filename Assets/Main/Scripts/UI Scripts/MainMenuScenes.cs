using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScenes : MonoBehaviour
{

    public GameObject mainMenuPage;
    public GameObject mainMenuOptionsPage;
    public GameObject GameLogo;
    public GameObject settingsPage;
    public GameObject guidePage;

    void Start()
    {
        ShowMainMenu(); //Show the Main Menu on Start
    }

    public void ShowMainMenu() //Show Main Menu
    {
        mainMenuPage.SetActive(true);
        GameLogo.SetActive(true);
        settingsPage.SetActive(false);
        guidePage.SetActive(false);
    }
    public void StartGame() //Start the game scene
    {
        SceneManager.LoadScene("SpaceshipGameScene");
    }

    public void OpenSettings() //Open Settings Panel
    {
        mainMenuOptionsPage.SetActive(false);
        GameLogo.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void CloseSettings() //Close Settings Panel
    {
        mainMenuOptionsPage.SetActive(true);
        GameLogo.SetActive(true);
        settingsPage.SetActive(false);
    }

    public void OpenGuide() //Open How to Play Panel
    {
        mainMenuOptionsPage.SetActive(false);
        GameLogo.SetActive(false);
        guidePage.SetActive(true);
    }

    public void CloseGuide() //Close How to Play Panel
    {
        mainMenuOptionsPage.SetActive(true);
        GameLogo.SetActive(true);
        guidePage.SetActive(false);
    }

    public void ExitGame() //Close the game
    {
        Application.Quit();
    }

    

}
