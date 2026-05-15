/// <summary>
/// Static utility class responsible for calculating and applying combat interactions.
/// </summary>
public static class CombatResolver
{
    /// <summary>
    /// Executes an attack from the attacker to the target, calculating damage and updating visuals.
    /// </summary>
    /// <param name="attacker">The entity performing the attack.</param>
    /// <param name="target">The entity receiving the attack.</param>
    /// <param name="damageDealt">Output parameter containing the amount of damage dealt.</param>
    public static void ResolveAttack(BattleEntities attacker, BattleEntities target, out int damageDealt)
    {
        // Calculate damage
        damageDealt = attacker.Strength; // TODO: Implement getDamage() function for complex stats
        
        // Apply damage and visuals
        attacker.BattleVisuals.PlayAttackAnimation();
        target.CurrentHealth -= damageDealt;
        target.BattleVisuals.PlayHitAnimation();
        target.UpdateUI();
    }
}
