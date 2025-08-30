using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    private void OnEnable()
    {
        tryAgainButton.onClick.AddListener(onTryAgainButtonClicked);
        quitButton.onClick.AddListener(onQuitButtonClicked);
        EventService.Instance.OnKeyPickedUp.AddListener(updateKeyText);
    }
    private void OnDisable()
    {
        EventService.Instance.OnKeyPickedUp.RemoveListener(updateKeyText);
    }
    public void UpdateInsanity(float playerSanity) => insanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);

    private void updateKeyText(int currentKeys) => keysFoundText.SetText($"Keys Found: {currentKeys}/3");
    private void onQuitButtonClicked() => Application.Quit();
    private void onTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

