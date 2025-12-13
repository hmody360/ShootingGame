using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScenes : MonoBehaviour
{

    public GameObject mainMenuPage;
    public GameObject settingsPage;

    void Start()
    {
        ShowMainMenu();

        float SavedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = SavedVolume;


    }

    public void ShowMainMenu()
    {
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SpaceshipGameScene");
    }

    public void OpenSettings()
    {
        settingsPage.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPage.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    

}
