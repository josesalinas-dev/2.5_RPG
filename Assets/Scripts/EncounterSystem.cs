using UnityEngine;
using RPGInterfaces;

/// <summary>
/// Sets up enemy encounters in a scene based on serialized encounter data.
/// Randomly generates enemies within specified level ranges when the scene loads.
/// </summary>
public class EncounterSystem : MonoBehaviour
{
    [SerializeField] private Encounter[] enemiesInScene;
    [SerializeField] private int maxNumEnemies;

    private IEnemyManager enemyManager;
    
    /// <summary>
    /// Initializes the encounter system and generates random enemies based on the encounter data.
    /// </summary>
    void Start()
    {
        enemyManager = ServiceLocator.GetService<IEnemyManager>();
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
