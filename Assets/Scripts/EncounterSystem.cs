<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
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
    
<<<<<<< HEAD
    /// <summary>
    /// Initializes the encounter system and generates random enemies based on the encounter data.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    void Start()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();
        enemyManager.GenerateEnemybyEncounter(enemiesInScene, maxNumEnemies);
    }
    
}

[System.Serializable]
<<<<<<< HEAD
=======

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
public class Encounter
{
    public EnemyInfo Enemy;
    public int LevelMin;
    public int LevelMax;
}
