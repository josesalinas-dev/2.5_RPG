using UnityEngine;

/// <summary>
/// Sets up enemy encounters in a scene based on serialized encounter data.
/// Randomly generates enemies within specified level ranges when the scene loads.
/// </summary>
public class EncounterSystem : MonoBehaviour
{
    [SerializeField] private Encounter[] enemiesInScene;
    [SerializeField] private int maxNumEnemies;

    private EnemyManager enemyManager;
    
    /// <summary>
    /// Initializes the encounter system and generates random enemies based on the encounter data.
    /// </summary>
    void Start()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();
        enemyManager.GenerateEnemybyEncounter(enemiesInScene, maxNumEnemies);
    }
    
}

[System.Serializable]
public class Encounter
{
    public EnemyInfo Enemy;
    public int LevelMin;
    public int LevelMax;
}
