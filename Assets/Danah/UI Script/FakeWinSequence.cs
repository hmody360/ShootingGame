using UnityEngine;

public class FakeWinSequence : MonoBehaviour
{
    public GameObject FakeWinPanel;  
    public GameObject FakeLosePanel;  

    public float winTime = 3f;        
    public float loseTime = 3f;      

    public void StartSequence()
    {
        StartCoroutine(WinThenLose());
    }

    IEnumerator WinThenLose()
    {
        FakeWinPanel.SetActive(true);
        FakeLosePanel.SetActive(false);
        yield return new WaitForSeconds(winTime);

        FakeWinPanel.SetActive(false);
        FakeLosePanel.SetActive(true);
        yield return new WaitForSeconds(loseTime);

    }

   
