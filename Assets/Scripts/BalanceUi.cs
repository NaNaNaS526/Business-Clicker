using TMPro;
using UnityEngine;

public class BalanceUi : MonoBehaviour
{
    #region Fields

    [SerializeField] private int startBalance;

    private TextMeshProUGUI _balanceText;
    private static int _currentBalance;

    #endregion

    #region Properties

    public static int CurrentBalance
    {
        get => _currentBalance;
        set
        {
            _currentBalance = value;
            if (_currentBalance < 0) _currentBalance = 0;
            EventBus.OnBalanceChanged($"Balance: {_currentBalance}$");
        }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        _balanceText = GetComponent<TextMeshProUGUI>();
        EventBus.OnBalanceChanged += UpdateCurrentBalance;
        CurrentBalance = startBalance;
    }

    private void UpdateCurrentBalance(string balance)
    {
        _balanceText.text = balance;
    }

    #endregion
}