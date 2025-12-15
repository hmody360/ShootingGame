using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class UIManger : MonoBehaviour
{
    //   UI RIFERENCE
    [Header("Bar")]
    public Image staminaFill;
    public Image HealthFill;

    [Header("HUD")]
    public GameObject HUD;
    public GameObject hudBase;
    public GameObject hudWeapon;

    [Header("Panels")]
    public GameObject collectiblesPanel;
    public GameObject DrawerMiniGamePanel;
    public GameObject PauseGamePanel;
    public GameObject SettingsPanel;
    public GameObject LosePanel;
    public GameObject winPanel;
    public GameObject trueEndPanel;
    public GameObject creditsPanel;

    [Header("Objective")]
    public Image[] collectibleSlots;
    public List<ObjectiveData> objectiveList;
    public TextMeshProUGUI objectiveListText;
    public TextMeshProUGUI PromptText;

    [Header("Ammo UI")]
    public TextMeshProUGUI currentAmmoUI;
    public TextMeshProUGUI maxAmmoUI;

    //Damage Screen
    [Header("UI Damege")]
    public GameObject damageOverlay;
    public Image heartIcon;
    public Image healthBarFill;

    [Header("Colors")]
    public Color normalColor;
    public Color damageColor;

    [Header("Timings")]
    public float effectDuration = 10f;
    public AudioSource lowHPSoundSource;

    [Header("Crosshair Sprites")]
    public Image crosshairUI;
    public Sprite normalCrosshair;
    public Sprite weaponCrosshair;
    [Header("Camera")]
    public GameObject _FPCamera;

    //bool lowHpSoundPlayed = false;
    Coroutine damageRoutine;

    public static UIManger instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this instance as the singleton instance
            instance = this;
        }

        lowHPSoundSource = GetComponent<AudioSource>();
    }


    void Start() //set the main UI elements
    {
        HUD.SetActive(true);
        hudBase.SetActive(true);
        hudWeapon.SetActive(false);
        initializeObjectiveList();
        updateObjectiveList();

        float SavedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = SavedVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGamePanel.activeSelf)
            {
                ResumeGame();
            }
            else if (!SettingsPanel.activeSelf && !winPanel.activeSelf && !trueEndPanel.activeSelf && !creditsPanel.activeSelf)
            {
                PauseGame();
            }
        }
    }

    //UI UPDATE METHODS
    public void UpdateAmmo(float CurrentAmmo, float MaxAmmo)
    {
        if (currentAmmoUI != null)
            currentAmmoUI.text = CurrentAmmo.ToString();

        if (maxAmmoUI != null)
            maxAmmoUI.text = MaxAmmo.ToString();
    }

    public void UpdateStamina(float currentStamina, float maxStamina)
    {
        StartCoroutine(SmoothBar(staminaFill, currentStamina / maxStamina));
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        StartCoroutine(SmoothBar(HealthFill, currentHealth / maxHealth));

        if ((currentHealth / maxHealth) <= 0.25)
        {
            ShowDamage();
        }
    }


    IEnumerator SmoothBar(Image bar, float target)
    {
        float current = bar.fillAmount;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 3;
            bar.fillAmount = Mathf.Lerp(current, target, t);
            yield return null;
        }
    }



    //UI Toggles 

    public void ToggleWeaponHUD()
    {
        bool state = hudWeapon.activeSelf;
        hudWeapon.SetActive(!state);

        crosshairUI.sprite = hudWeapon.activeSelf ? weaponCrosshair : normalCrosshair;

    }

    public void ToggleCollectibles()
    {
        bool state = collectiblesPanel.activeSelf;
        collectiblesPanel.SetActive(!state);
    }

    //UI Screen
    public void LoseScreen()
    {
        HUD.SetActive(false);
        LosePanel.SetActive(true);
        CloseDrawerGameAfterLoss();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    public void winScreen()
    {
        HUD.SetActive(false);
        StartCoroutine(WinThenLose());
    }

    public void CreditsScreen()
    {
        trueEndPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }


    //Collect and use Items
    public void AddCollectible(Sprite itemIcon)
    {
        for (int i = 0; i < collectibleSlots.Length; i++)
        {
            if (collectibleSlots[i].sprite == null)
            {
                collectibleSlots[i].sprite = itemIcon;
                collectibleSlots[i].color = Color.white;
                return;
            }
        }
    }

    public void RemoveCollectible(Sprite itemIcon)
    {
        for (int i = 0; i < collectibleSlots.Length; i++)
        {
            if (collectibleSlots[i].sprite == itemIcon)
            {
                collectibleSlots[i].sprite = null;
                collectibleSlots[i].color = Color.clear;
                return;
            }
        }
    }

    // damege Screen

    public void ShowDamage()
    {
        if (damageRoutine != null) StopCoroutine(damageRoutine);

        damageRoutine = StartCoroutine(DamageEffect());
    }

    public void updateObjectiveList()
    {
        objectiveListText.text = "";
        foreach (ObjectiveData objective in objectiveList)
        {
            if (objective.isActive)
            {
                objectiveListText.text += objective.objectiveText + "\n";
            }
        }
    }

    public void initializeObjectiveList()
    {
        foreach (ObjectiveData objective in objectiveList)
        {
            if (objective.id == 1)
            {
                objective.isActive = true;
            }
            else
            {
                objective.isActive = false;
            }

        }
    }

    public void StartDrawerMinigame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        DrawerMiniGamePanel.SetActive(true);
        _FPCamera.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = false;
        Camera.main.gameObject.GetComponent<RayInetractor>().enabled = false;
    }

    public void CloseDrawerMiniGame()
    {
        DrawerMiniGamePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("IDDrawer").GetComponent<IDDrawerActivator>().CloseGame();
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = true;
        _FPCamera.SetActive(true);
        Camera.main.gameObject.GetComponent<RayInetractor>().enabled = true;
    }

    private void CloseDrawerGameAfterLoss()
    {
        DrawerMiniGamePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("IDDrawer").GetComponent<IDDrawerActivator>().CloseGame();
        _FPCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.gameObject.GetComponent<RayInetractor>().enabled = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
        _FPCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Camera.main.gameObject.GetComponent<RayInetractor>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = false;
        if (DrawerMiniGamePanel.activeSelf)
        {
            CloseDrawerMiniGame();
        }
        PauseGamePanel.SetActive(true);
    }

    public void ResumeGame()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Time.timeScale = 1f;
        HUD.SetActive(true);
        _FPCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.gameObject.GetComponent<RayInetractor>().enabled = true;
        player.GetComponent<PlayerMovement>().canMove = true;
        UpdateHealth(playerHealth.currentHealth, playerHealth.maxHealth);
        PauseGamePanel.SetActive(false);
    }

    public void OpenSettingsInGame()
    {
        PauseGamePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void CloseSettingsInGame()
    {
        PauseGamePanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void ShowPromptText(string text)
    {
        PromptText.text = text;
    }

    public void HidePromptText()
    {
        PromptText.text = "";
    }

    IEnumerator DamageEffect()
    {
        //Show damage screen
        damageOverlay.SetActive(true);
        healthBarFill.color = damageColor;

        //If health is low -> play warning sound once
        lowHPSoundSource.Play();

        //Wait before fading UI back to normal
        yield return new WaitForSeconds(effectDuration);

        //Reset UI effects back to default state
        damageOverlay.SetActive(false);
        healthBarFill.color = normalColor;

        lowHPSoundSource.Stop();


    }

    IEnumerator WinThenLose()
    {
        winPanel.SetActive(true);
        trueEndPanel.SetActive(false);
        yield return new WaitForSeconds(8f);

        winPanel.SetActive(false);
        trueEndPanel.SetActive(true);
        StopCoroutine(WinThenLose());
    }



}
