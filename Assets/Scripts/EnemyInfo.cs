using UnityEngine;

/// <summary>
/// ScriptableObject that defines enemy data (stats, visuals, prefabs).
/// Used to create reusable enemy templates in the asset pipeline.
/// </summary>
[CreateAssetMenu(menuName = "NewEnemy")]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public int baseHealth;
    public int baseStr;
    public int baseInitiative;
    public GameObject enemyBattleVisualPrefab;
    public GameObject enemyDungeonVisualPrefab;
}
