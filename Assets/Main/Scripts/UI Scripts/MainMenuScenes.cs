using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScenes : MonoBehaviour
{

    public GameObject mainMenuPage;
    public GameObject settingsPage;

    void Start()
    {
        ShowMainMenu(); //Show the Main Menu on Start


    }

    public void ShowMainMenu() //Show Main Menu
    {
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
    }
    public void StartGame() //Start the game scene
    {
        SceneManager.LoadScene("SpaceshipGameScene");
    }

    public void OpenSettings() //Open Settings Panel
    {
        settingsPage.SetActive(true);
    }

    public void CloseSettings() //Close Settings Panel
    {
        settingsPage.SetActive(false);
    }

    public void ExitGame() //Close the game
    {
        Application.Quit();
    }

    

}
