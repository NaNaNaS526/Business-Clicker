using UnityEngine.Events;

public static class EventBus
{
    #region UnityActions

    public static UnityAction<string, int> OnNameChanged;
    public static UnityAction<string, int> OnLevelChanged;
    public static UnityAction<string, int> OnIncomeChanged;
    public static UnityAction<string, int> OnLevelUpCostChanged;
    public static UnityAction<string, int, int> OnEnhancementChanged;
    public static UnityAction<string> OnBalanceChanged;
    public static UnityAction<float, int> OnIncreaseBarValueChanged;
    public static UnityAction<int> OnLevelUpTried;
    public static UnityAction<int, int> OnEnhancementBuyTried;

    #endregion
}