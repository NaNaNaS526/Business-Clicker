using System.Collections;
using UnityEngine;

public class Business : MonoBehaviour
{
    #region Fields

    [SerializeField] private BusinessData data;
    [SerializeField] private int businessIndex;

    private string _businessName;
    private int _level;
    private int _incomeDelay;
    private int _levelUpCost;
    private int _income;
    private string _firstEnhancementName;
    private int _firstEnhancementCost;
    private int _firstEnhancementMultiplier;
    private string _secondEnhancementName;
    private int _secondEnhancementCost;
    private int _secondEnhancementMultiplier;

    private float _currentIncomeBarValue;
    private bool _isFirstEnhancementBought;
    private bool _isSecondEnhancementBought;

    #endregion

    #region Properties

    private string BusinessName
    {
        set
        {
            _businessName = value;
            EventBus.OnNameChanged(_businessName, businessIndex);
        }
    }

    private int Level
    {
        get => _level;
        set
        {
            _level = value;
            if (_level >= 1)
            {
                StopAllCoroutines();
                StartCoroutine(IncreasingIncome());
            }

            EventBus.OnLevelChanged($"Level \n {_level}", businessIndex);
        }
    }

    private int Income
    {
        get => _income;
        set
        {
            _income = value;
            EventBus.OnIncomeChanged($"Income\n {_income}$", businessIndex);
        }
    }

    private int LevelUpCost
    {
        set
        {
            _levelUpCost = value;
            EventBus.OnLevelUpCostChanged($"Level Up \n {_levelUpCost}$", businessIndex);
        }
    }

    private string FirstEnhancementName
    {
        set
        {
            _firstEnhancementName = value;
            EventBus.OnEnhancementChanged(
                $"{_firstEnhancementName}\n Income: +{data.firstEnhancementIncomeMultiplier}%\n Cost: {_firstEnhancementCost}$",
                businessIndex, 1);
        }
    }

    private int FirstEnhancementMultiplier
    {
        get => _firstEnhancementMultiplier;
        set
        {
            _firstEnhancementMultiplier = value;
            EventBus.OnEnhancementChanged(
                !_isFirstEnhancementBought
                    ? $"{_firstEnhancementName}\n Income: +{data.firstEnhancementIncomeMultiplier}%\n Cost: {_firstEnhancementCost}$"
                    : $"{_firstEnhancementName}\n Income: +{data.firstEnhancementIncomeMultiplier}%\n Bought",
                businessIndex, 1);
        }
    }

    private int FirstEnhancementCost
    {
        get => _firstEnhancementCost;
        set
        {
            _firstEnhancementCost = value;
            EventBus.OnEnhancementChanged(
                !_isFirstEnhancementBought
                    ? $"{_firstEnhancementName}\n Income: +{data.firstEnhancementIncomeMultiplier}%\n Cost: {_firstEnhancementCost}$"
                    : $"{_firstEnhancementName}\n Income: +{data.firstEnhancementIncomeMultiplier}%\n Bought",
                businessIndex, 1);
        }
    }

    private string SecondEnhancementName
    {
        set
        {
            _secondEnhancementName = value;
            EventBus.OnEnhancementChanged(
                $"{_secondEnhancementName}\n Income: +{_secondEnhancementMultiplier}%\n Cost: {_secondEnhancementCost}$",
                businessIndex, 2);
        }
    }

    private int SecondEnhancementMultiplier
    {
        get => _secondEnhancementMultiplier;
        set
        {
            _secondEnhancementMultiplier = value;
            EventBus.OnEnhancementChanged(
                !_isSecondEnhancementBought
                    ? $"{_secondEnhancementName}\n Income: +{data.secondEnhancementIncomeMultiplier}%\n Cost: {_secondEnhancementCost}$"
                    : $"{_secondEnhancementName}\n Income: +{data.secondEnhancementIncomeMultiplier}%\n Bought",
                businessIndex, 2);
        }
    }

    private int SecondEnhancementCost
    {
        get => _secondEnhancementCost;
        set
        {
            _secondEnhancementCost = value;
            EventBus.OnEnhancementChanged(
                !_isSecondEnhancementBought
                    ? $"{_secondEnhancementName}\n Income: +{data.secondEnhancementIncomeMultiplier}%\n Cost: {_secondEnhancementCost}$"
                    : $"{_secondEnhancementName}\n Income: +{data.secondEnhancementIncomeMultiplier}%\n Bought",
                businessIndex, 2);
        }
    }

    #endregion

    #region Methods

    private void Start()
    {
        BusinessName = data.businessName;
        Level = data.baseLevel;
        _incomeDelay = data.incomeDelay;
        LevelUpCost = data.baseCost;
        Income = data.baseIncome;
        FirstEnhancementName = data.firstEnhancementName;
        FirstEnhancementCost = data.firstEnhancementCost;
        FirstEnhancementMultiplier = 0;
        SecondEnhancementName = data.secondEnhancementName;
        SecondEnhancementCost = data.secondEnhancementCost;
        SecondEnhancementMultiplier = 0;

        LevelUpCost = _levelUpCost;
        EventBus.OnLevelUpTried += TryLevelUp;
        EventBus.OnEnhancementBuyTried += TryBuyEnhancement;
    }

    private void TryLevelUp(int index)
    {
        if (businessIndex != index) return;
        if (BalanceUi.CurrentBalance < _levelUpCost) return;
        Level += 1;
        BalanceUi.CurrentBalance -= _levelUpCost;
        LevelUpCost = (Level + 1) * data.baseCost;
        Income = Level * data.baseIncome * (1 + _firstEnhancementMultiplier + _secondEnhancementMultiplier);
    }

    private void TryBuyEnhancement(int index, int indexOfBusiness)
    {
        switch (index)
        {
            case 1 when FirstEnhancementCost <= BalanceUi.CurrentBalance && indexOfBusiness == businessIndex &&
                        !_isFirstEnhancementBought:

                _isFirstEnhancementBought = true;
                FirstEnhancementCost = 0;
                FirstEnhancementMultiplier = data.firstEnhancementIncomeMultiplier;
                Income = Level * data.baseIncome * (1 + FirstEnhancementMultiplier + SecondEnhancementMultiplier);
                BalanceUi.CurrentBalance -= _firstEnhancementCost;
                break;
            case 2 when SecondEnhancementCost <= BalanceUi.CurrentBalance && indexOfBusiness == businessIndex &&
                        !_isSecondEnhancementBought:

                _isSecondEnhancementBought = true;
                SecondEnhancementCost = 0;
                SecondEnhancementMultiplier = data.secondEnhancementIncomeMultiplier;
                Income = Level * data.baseIncome * (1 + FirstEnhancementMultiplier + SecondEnhancementMultiplier);
                BalanceUi.CurrentBalance -= _secondEnhancementCost;
                break;
        }
    }

    #endregion

    #region Coroutines

    private IEnumerator IncreasingIncome()
    {
    start:
        _currentIncomeBarValue = 0;
        while (_currentIncomeBarValue < 1)
        {
            yield return new WaitForSeconds(1 / 1000f);
            _currentIncomeBarValue += _incomeDelay / 1000f;
            EventBus.OnIncreaseBarValueChanged(_currentIncomeBarValue, businessIndex);
        }

        BalanceUi.CurrentBalance += Income;
        goto start;
    }

    #endregion
}