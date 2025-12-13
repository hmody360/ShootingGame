using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IDCardGameManager : MonoBehaviour
{
    [System.Serializable]
    public class DrawerItemData
    {
        public string name;

        public Sprite icon;

        public bool isIdCard;
    }

    [Header("UI References")]
    [SerializeField] private RectTransform drawerContainer;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshProUGUI clicksText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI foundText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 30f;

    [SerializeField] private List<DrawerItemData> gameItems = new List<DrawerItemData>();

    private int clickCount = 0;
    private float remainingTime;

    private bool isGameActive = false;
    private bool hasWon = false;
    private List<GameObject> spawnedItems = new List<GameObject>();
    private GameUIManager uiManager;

    void Awake()
    {
        uiManager = GetComponent<GameUIManager>();
        if (uiManager == null)
        {
            uiManager = gameObject.AddComponent<GameUIManager>();
        }
    }

    void Start()
    {
        StartNewGame();
    }

    void Update()
    {
        if (!isGameActive || hasWon) return;

        //COUNTDOWN LOGIC
        remainingTime -= Time.deltaTime;
        UpdateTimer(remainingTime);

        if (remainingTime <= 0f)
        {
            HandleLose();
        }
    }

    public void StartNewGame()
    {
        ClearExistingItems();
        ResetGameState();
        SpawnItems();
        UpdateUI();
    }

    private void ClearExistingItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        spawnedItems.Clear();
    }

    private void ResetGameState()
    {
        clickCount = 0;
        hasWon = false;
        isGameActive = true;

        //RESET TIMER
        remainingTime = gameDuration;
    }

    private void SpawnItems()
    {
        if (drawerContainer == null || itemPrefab == null)
        {
            Debug.LogWarning("Drawer container or item prefab not assigned!");
            return;
        }

        List<DrawerItemData> shuffledItems = new List<DrawerItemData>(gameItems);
        ShuffleList(shuffledItems);

        Rect drawerRect = drawerContainer.rect;
        float padding = 50f;
        float itemSize = 140f;
        float halfItemSize = itemSize * 0.5f;

        float minX = -drawerRect.width * 0.5f + padding + halfItemSize;
        float maxX = drawerRect.width * 0.5f - padding - halfItemSize;
        float minY = -drawerRect.height * 0.5f + padding + halfItemSize;
        float maxY = drawerRect.height * 0.5f - padding - halfItemSize;

        foreach (DrawerItemData itemData in shuffledItems)
        {
            GameObject itemObj = Instantiate(itemPrefab, drawerContainer);
            DrawerItemController itemController = itemObj.GetComponent<DrawerItemController>();

            if (itemController == null)
            {
                itemController = itemObj.AddComponent<DrawerItemController>();
            }

            RectTransform itemRect = itemObj.GetComponent<RectTransform>();
            itemRect.anchorMin = itemRect.anchorMax = itemRect.pivot = new Vector2(0.5f, 0.5f);

            itemController.Initialize(itemData, this);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            itemRect.anchoredPosition = new Vector2(randomX, randomY);
            itemRect.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            spawnedItems.Add(itemObj);
        }
    }

    public void OnItemClicked(DrawerItemData itemData)
    {
        if (!isGameActive || hasWon) return;

        clickCount++;
        UpdateClickCounter();

        if (itemData.isIdCard)
        {
            HandleWin();
        }
        else
        {
            ShowMessage($"That's a {itemData.name}, keep looking!", Color.white);
        }
    }

    private void HandleWin()
    {
        hasWon = true;
        isGameActive = false;



        ShowMessage(
            $"You found the ID card!",
            Color.green
        );

        UpdateFoundStatus(true);
        UIManger.instance.CloseDrawerMiniGame();
        GameObject.FindGameObjectWithTag("IDDrawer").GetComponent<IDDrawerActivator>().OpenDrawer();
    }

    // ❌ NEW: LOSE CONDITION
    private void HandleLose()
    {
        isGameActive = false;

        ShowMessage(
            "⏰ Time's up! You lost!",
            Color.red
        );
        StartNewGame();
        UIManger.instance.CloseDrawerMiniGame();
    }

    private void UpdateUI()
    {
        UpdateClickCounter();
        UpdateTimer(remainingTime);
        UpdateFoundStatus(false);
        ShowMessage("", Color.white);
    }

    private void UpdateClickCounter()
    {
        clicksText.text = clickCount.ToString();
        uiManager?.UpdateClicks(clickCount);
    }

    private void UpdateTimer(float time)
    {
        time = Mathf.Max(0, time);
        timeText.text = Mathf.CeilToInt(time) + "s";
        uiManager?.UpdateTimer(time);
    }

    private void UpdateFoundStatus(bool found)
    {
        foundText.text = found ? "1/1" : "0/1";
        uiManager?.UpdateFoundStatus(found);
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.text = message;
        messageText.color = color;
        uiManager?.ShowMessage(message, color);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public bool HasWon()
    {
        return hasWon;
    }
}
