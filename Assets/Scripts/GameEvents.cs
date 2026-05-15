using System;

/// <summary>
/// Enumerates the possible outcomes of a battle.
/// </summary>
public enum BattleResult
{
    Won,
    Lost,
    Run
}

/// <summary>
/// Static Event Bus for global game events. 
/// Decouples systems from direct scene loading or cross-system references.
/// </summary>
public static class GameEvents
{
    public static event Action OnBattleTriggered;
    public static event Action<BattleResult> OnBattleResolved;
    public static event Action OnGameWon;

    public static void TriggerBattle()
    {
        OnBattleTriggered?.Invoke();
    }

    public static void ResolveBattle(BattleResult result)
    {
        OnBattleResolved?.Invoke(result);
    }

    public static void WinGame()
    {
        OnGameWon?.Invoke();
    }
}
