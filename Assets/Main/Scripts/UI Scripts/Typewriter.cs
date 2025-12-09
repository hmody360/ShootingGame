using UnityEngine;
using TMPro;
using System.Collections;

public class TwoLinesAutoWriter : MonoBehaviour
{
    [Header("Text Fields From UI")]
    public TMP_Text line1Text;
    public TMP_Text line2Text;

    [Header("Typing Settings")]
    public float speed = 0.05f;
    public float delayBetweenLines = 0.7f;

    string originalLine1;
    string originalLine2;

    private void OnEnable()
    {
        originalLine1 = line1Text.text;  
        originalLine2 = line2Text.text;   

        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
       
        line1Text.text = "";
        line2Text.text = "";

        foreach (char c in originalLine1)
        {
            line1Text.text += c;
            yield return new WaitForSeconds(speed);
        }

       
        yield return new WaitForSeconds(delayBetweenLines);

       
        foreach (char c in originalLine2)
        {
            line2Text.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}
