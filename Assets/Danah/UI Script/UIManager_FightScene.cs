//using UnityEngine;
//using UnityEngine.UI;

//public class UIManager_FightScene : MonoBehaviour
//{

//    [SerializeField] private Image playerHealthFill;
//    [SerializeField] private Text playerHealthCounter;
//    [SerializeField] private Image playerAttackIndicator;

//    [SerializeField] private Image friendHealthFill;
//    [SerializeField] private Text friendHealthCounter;

//    [SerializeField] private Text enemyCounter;


//    public static UIManager_FightScene instance;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created

//    private void Awake()
//    {
//        instance = this;
//        updateEnemyCount(gameManager.instance._defeatedBullies, gameManager.instance._maxBullies);

//        if (instance == null) // Prevent Duplicate UI Managers
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//        ;
//    }

//    // Update is called once per frame

//    public void updatePlayerHealth(float currentHealth, float maxHealth)
//    {
//        float fillAmount = currentHealth / maxHealth;
//        playerHealthFill.fillAmount = fillAmount;
//        playerHealthCounter.text = currentHealth + " / " + maxHealth;

//        if (fillAmount <= 0.75f)
//        {
//            playerHealthFill.color = UnityEngine.Color.yellow;
//        }
//        else if (fillAmount <= 0.25f)
//        {
//            playerHealthFill.color = new UnityEngine.Color(255, 0, 0);
//        }
//        else
//        {
//            playerHealthFill.color = UnityEngine.Color.green;
//        }
//    }

//    public void updateAttackIndicator(bool isActive)
//    {
//        if (isActive)
//        {
//            playerAttackIndicator.color = Color.lightGreen;

//        }
//        else
//        {
//            playerAttackIndicator.color = Color.red;
//        }
//    }
//    public void updateFriendHealth(float currentHealth, float maxHealth)
//    {
//        float fillAmount = currentHealth / maxHealth;
//        friendHealthFill.fillAmount = fillAmount;
//        friendHealthCounter.text = currentHealth + " / " + maxHealth;

//        if (fillAmount <= 0.75f)
//        {
//            friendHealthFill.color = UnityEngine.Color.yellow;
//        }
//        else if (fillAmount <= 0.25f)
//        {
//            friendHealthFill.color = new UnityEngine.Color(255, 0, 0);
//        }
//        else
//        {
//            friendHealthFill.color = UnityEngine.Color.green;
//        }
//    }

//    public void updateEnemyCount(float currentCount, float maxCount)
//    {

//        enemyCounter.text = currentCount + " / " + maxCount;
//    }
//}
