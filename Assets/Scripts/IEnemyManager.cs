using System.Collections.Generic;
using UnityEngine;

namespace RPGInterfaces
{
    /// <summary>
    /// Interface for enemy manager functionality.
    /// Provides methods for enemy generation, battle persistence, and current enemy access.
    /// </summary>
    public interface IEnemyManager
    {
        /// <summary>
        /// Generates enemies based on encounter definitions and a maximum count.
        /// </summary>
        /// <param name="encounters">The encounter definitions to use for generation.</param>
        /// <param name="maxNumEnemies">The maximum number of enemies to generate.</param>
        void GenerateEnemybyEncounter(Encounter[] encounters, int maxNumEnemies);

        /// <summary>
        /// Generates an enemy by name and level and adds it to the current enemy list.
        /// </summary>
        /// <param name="enemyName">The name of the enemy type to generate.</param>
        /// <param name="level">The level for the generated enemy.</param>
        void GenerateEnemybyName(string enemyName, int level);

        /// <summary>
        /// Saves dungeon enemy state before a battle transition.
        /// </summary>
        /// <param name="enemyAttackerTransform">The transform of the attacking enemy.</param>
        void SaveDGEnemiesData(Transform enemyAttackerTransform);

        /// <summary>
        /// Provides the current list of active enemies for battle creation.
        /// </summary>
        /// <returns>The list of current enemies.</returns>
        List<Enemy> GetCurrentEnemies();

        /// <summary>
        /// Indicates whether the last battle was won.
        /// </summary>
        bool HasWonBattle { get; set; }
    }
}
