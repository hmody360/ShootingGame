using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScenes : MonoBehaviour
{

    public GameObject mainMenuPage;
    public GameObject settingsPage;

    void Start()
    {
        ShowMainMenu();

    }

    public void ShowMainMenu()
    {
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
    }
    public void StartGame()
    {
        mainMenuPage.SetActive(false);
        SceneManager.LoadScene("GameScene");
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

    

}
