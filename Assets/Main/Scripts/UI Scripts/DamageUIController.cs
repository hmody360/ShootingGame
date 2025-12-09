using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIController : MonoBehaviour
{
    [Header("UI REFERENCES")]
    public Image damageOverlay;
    public Image heartIcon;
    public Image healthBarFill;

    [Header("Colors")]
    public Color normalColor ;
    public Color damageColor ;

    [Header("Timings")]
    public float effectDuration = 0.6f;   
    public float lowHealthThreshold = 0.25f;

    [Header("Sound")]
    public AudioSource lowHpWarningSound;

    bool lowHpSoundPlayed = false;
    Coroutine damageRoutine;


    public void ShowDamage(float currentHP, float maxHP)
    {
        float hpPercent = currentHP / maxHP;

        if (damageRoutine != null) StopCoroutine(damageRoutine);

        damageRoutine = StartCoroutine(DamageEffect(hpPercent));
    }

    IEnumerator DamageEffect(float hpPercent)
    {
        damageOverlay.enabled = true;
        heartIcon.color = damageColor;
        healthBarFill.color = damageColor;

        if (hpPercent <= lowHealthThreshold)
        {
            if (!lowHpSoundPlayed && lowHpWarningSound != null)
            {
                lowHpWarningSound.Play();
                lowHpSoundPlayed = true; 
            }
        }
        else
        {
            lowHpSoundPlayed = false;
        }

        yield return new WaitForSeconds(effectDuration);

        damageOverlay.enabled = false;
        heartIcon.color = normalColor;
        healthBarFill.color = normalColor;

        
    }

    //public void HealEffect(float currentHP, float maxHP)
    //{
    //    float hpPercent = currentHP / maxHP;

    //    if (hpPercent > lowHealthThreshold)
    //    {
    //        damageOverlay.enabled = false;
    //        heartIcon.color = normalColor;
    //        healthBarFill.color = normalColor;
    //        lowHpSoundPlayed = false; 
    //    }
    //}
}