using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DrawerItemController : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IDCardGameManager.DrawerItemData itemData;
    private IDCardGameManager gameManager;


    private Image itemImage;
    private Image backgroundImage;

    private RectTransform rectTransform;
    private bool isAnimating = false;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    public void Initialize(IDCardGameManager.DrawerItemData data, IDCardGameManager manager)
    {
        itemData = data;
        gameManager = manager;

        SetupComponents();
        SetupAppearance();
        StoreOriginalTransform();
    }

    private void SetupComponents()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
            rectTransform = gameObject.AddComponent<RectTransform>();

        backgroundImage = GetComponent<Image>();
        if (backgroundImage == null)
            backgroundImage = gameObject.AddComponent<Image>();

        // 🔁 CREATE IMAGE FOR ITEM ICON
        itemImage = GetComponentInChildren<Image>();
        if (itemImage == null || itemImage.gameObject == gameObject)
        {
            CreateItemImage();
        }

        Button button = GetComponent<Button>();
        if (button == null)
            button = gameObject.AddComponent<Button>();

        button.transition = Selectable.Transition.None;
    }

    private void CreateItemImage()
    {
        GameObject iconObj = new GameObject("ItemIcon");
        iconObj.transform.SetParent(transform, false);

        RectTransform iconRect = iconObj.AddComponent<RectTransform>();
        iconRect.anchorMin = Vector2.zero;
        iconRect.anchorMax = Vector2.one;
        iconRect.offsetMin = Vector2.zero;
        iconRect.offsetMax = Vector2.zero;

        itemImage = iconObj.AddComponent<Image>();
        itemImage.preserveAspect = true;
        itemImage.raycastTarget = false;
    }

    private void SetupAppearance()
    {
        // 🔁 ASSIGN SPRITE
        itemImage.sprite = itemData.icon;
        itemImage.color = Color.white;

        if (itemData.isIdCard)
            SetupIdCardAppearance();
        else
            SetupRegularItemAppearance();

        rectTransform.sizeDelta = new Vector2(140, 140);
    }

    private void SetupIdCardAppearance()
    {
        backgroundImage.color = Color.clear;
    }

    private void SetupRegularItemAppearance()
    {
        backgroundImage.color = Color.clear;
    }

    private void StoreOriginalTransform()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAnimating || !gameManager.IsGameActive() || gameManager.HasWon())
            return;

        gameManager.OnItemClicked(itemData);

        if (itemData.isIdCard)
        {

        }
        else
            StartCoroutine(ShakeThenDestroyAnimation());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isAnimating)
            transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isAnimating)
            transform.localScale = originalScale;
    }

    private IEnumerator ShakeThenDestroyAnimation()
    {
        isAnimating = true;
        float duration = 0.4f;
        float intensity = 10f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);
            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            float rot = Random.Range(-6f, 6f);
            transform.localRotation = originalRotation * Quaternion.Euler(0, 0, rot);

            elapsed += Time.deltaTime;
            yield return null;
        }

        isAnimating = false;
        Destroy(gameObject);
    }

    private IEnumerator CelebrateAnimation()
    {
        isAnimating = true;
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float scale = 1f + Mathf.Sin(elapsed * 8f) * 0.3f;
            transform.localScale = originalScale * scale;

            transform.localRotation =
                originalRotation * Quaternion.Euler(0, 0, elapsed * 720f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        transform.localRotation = originalRotation;
        isAnimating = false;
    }
}
