using UnityEngine;

[CreateAssetMenu(fileName = "BusinessData")]
public class BusinessData : ScriptableObject
{
    #region Fields

    public string businessName;
    public int baseLevel;
    public int incomeDelay;
    public int baseCost;
    public int baseIncome;
    public string firstEnhancementName;
    public int firstEnhancementCost;
    public int firstEnhancementIncomeMultiplier;
    public string secondEnhancementName;
    public int secondEnhancementCost;
    public int secondEnhancementIncomeMultiplier;

    #endregion
}