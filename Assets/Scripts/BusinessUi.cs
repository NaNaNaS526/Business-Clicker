using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessUi : MonoBehaviour
{
    #region Fields

    [SerializeField] private int businessIndex;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image incomeBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button firstEnhancementButton;
    [SerializeField] private Button secondEnhancementButton;

    private TextMeshProUGUI _levelUpText;
    private TextMeshProUGUI _firstEnhancementText;
    private TextMeshProUGUI _secondEnhancementText;

    private const int FirstEnhancementIndex = 1;
    private const int SecondEnhancementIndex = 2;

    #endregion

    #region Methods

    private void Awake()
    {
        _levelUpText = levelUpButton.GetComponentInChildren<TextMeshProUGUI>();
        _firstEnhancementText = firstEnhancementButton.GetComponentInChildren<TextMeshProUGUI>();
        _secondEnhancementText = secondEnhancementButton.GetComponentInChildren<TextMeshProUGUI>();

        EventBus.OnNameChanged += UpdateBusinessNameText;
        EventBus.OnLevelChanged += UpdateLevelText;
        EventBus.OnIncomeChanged += UpdateIncomeText;
        EventBus.OnLevelUpCostChanged += UpdateLevelUpText;
        EventBus.OnEnhancementChanged += UpdateEnhancement;
        EventBus.OnIncreaseBarValueChanged += IncomeBarFilling;
        levelUpButton.onClick.AddListener(TryLevelUp);
        firstEnhancementButton.onClick.AddListener(TryBuyFirstEnhancement);
        secondEnhancementButton.onClick.AddListener(TryBuySecondEnhancement);
    }

    private void UpdateBusinessNameText(string newName, int index)
    {
        if (businessIndex == index) nameText.text = newName;
    }

    private void UpdateLevelText(string newLevel, int index)
    {
        if (businessIndex == index) levelText.text = newLevel;
    }

    private void UpdateIncomeText(string newIncome, int index)
    {
        if (businessIndex == index) incomeText.text = newIncome;
    }

    private void UpdateLevelUpText(string newCost, int index)
    {
        if (businessIndex == index) _levelUpText.text = newCost;
    }

    private void UpdateEnhancement(string newName, int index, int textIndex)
    {
        if (businessIndex != index) return;
        switch (textIndex)
        {
            case 1:
                _firstEnhancementText.text = newName;
                break;
            case 2:
                _secondEnhancementText.text = newName;
                break;
        }
    }

    private void IncomeBarFilling(float filling, int index)
    {
        if (businessIndex == index) incomeBar.fillAmount = filling;
    }

    private void TryLevelUp()
    {
        EventBus.OnLevelUpTried(businessIndex);
    }

    private void TryBuyFirstEnhancement()
    {
        EventBus.OnEnhancementBuyTried(FirstEnhancementIndex, businessIndex);
    }

    private void TryBuySecondEnhancement()
    {
        EventBus.OnEnhancementBuyTried(SecondEnhancementIndex, businessIndex);
    }

    #endregion
}