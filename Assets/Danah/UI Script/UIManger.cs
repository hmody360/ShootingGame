using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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
    public GameObject LosePanel;
    public GameObject winPanel;


    [Header("Objective")]
    public Image[] collectibleSlots;

    [Header("Ammo UI")]
    public Text currentAmmoUI;
    public Text maxAmmoUI;

    //Damage Screen
    [Header("UI Damege")]
    public Image damageOverlay;
    public Image heartIcon;
    public Image healthBarFill;

    [Header("Colors")]
    public Color normalColor;
    public Color damageColor;

    [Header("Timings")]
    public float effectDuration = 0.6f;
    public float lowHealthThreshold = 0.25f;

    [Header("Crosshair Sprites")]
    public Image crosshairUI;
    public Sprite normalCrosshair;
    public Sprite weaponCrosshair;
    //[Header("Sound")]
    //public AudioSource lowHpWarningSound;

    //bool lowHpSoundPlayed = false;
    Coroutine damageRoutine;


    void Start()
    {
        HUD.SetActive(true);
        hudBase.SetActive(true);
        hudWeapon.SetActive(true);
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
    }
    public void winScreen()
    {
        HUD.SetActive(false);
        winPanel.SetActive(true);
    }


    //Collect Items
    public void AddCollectible(Sprite itemIcon)
    {
        for (int i = 0; i < collectibleSlots.Length; i++)
        {
            if (collectibleSlots[i].sprite == null)
            {
                collectibleSlots[i].sprite = itemIcon;
                return;
            }
        }

    }

    // damege Screen

    public void ShowDamage(float currentHP, float maxHP)
    {
        UpdateHealth(currentHP, maxHP);
       //  hpPercent = currentHP / maxHP;
        if (damageRoutine != null) StopCoroutine(damageRoutine);

        damageRoutine = StartCoroutine(DamageEffect());
    }

    IEnumerator DamageEffect()
    {
        //Show damage screen
        damageOverlay.enabled = true;
        heartIcon.color = damageColor;
        healthBarFill.color = damageColor;

        ////If health is low -> play warning sound once
        //if (hpPercent <= lowHealthThreshold)
        //{
        //    if (!lowHpSoundPlayed && lowHpWarningSound != null)
        //    {
        //        lowHpWarningSound.Play();
        //        //prevent sound from repeating
        //        lowHpSoundPlayed = true;
        //    }
        //}
        //else
        //{
        //    lowHpSoundPlayed = false; // reset 
        //}

        //Wait before fading UI back to normal
        yield return new WaitForSeconds(effectDuration);

        //Reset UI effects back to default state
        damageOverlay.enabled = false;
        heartIcon.color = normalColor;
        healthBarFill.color = normalColor;


    }

}//end class
