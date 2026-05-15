using System.Collections.Generic;
using UnityEngine;

namespace RPGInterfaces
{
    /// <summary>
    /// Interface for party management functionality.
    /// Handles party composition, health updates, and player position persistence.
    /// </summary>
    public interface IPartyManager
    {
        /// <summary>
        /// Adds a new party member to the active party by searching for their data by name.
        /// </summary>
        /// <param name="memberName">The name of the party member to add.</param>
        void AddMembertoPartyByName(string memberName);

        /// <summary>
        /// Returns a list of currently alive party members (with health > 0).
        /// </summary>
        /// <returns>A list of party members with positive health.</returns>
        List<PartyMember> GetAliveParty();

        /// <summary>
        /// Returns the current list of active party members.
        /// </summary>
        /// <returns>The list of party members currently in the party.</returns>
        List<PartyMember> GetCurrentParty();

        /// <summary>
        /// Updates the health value of a specific party member after a battle.
        /// </summary>
        /// <param name="partyMember">The index of the party member to update.</param>
        /// <param name="health">The new health value to set.</param>
        void SaveHealth(int partyMember, int health);

        /// <summary>
        /// Sets the player's position, typically saved when transitioning to a battle.
        /// </summary>
        /// <param name="position">The position to save.</param>
        void SetPosition(Vector3 position);

        /// <summary>
        /// Retrieves the previously saved player position.
        /// </summary>
        /// <returns>The saved player position.</returns>
        Vector3 GetPosition();

        /// <summary>
        /// Retrieves the default starting position for the player in the overworld.
        /// </summary>
        /// <returns>The default start position.</returns>
        Vector3 GetPlayerStartPosition();
    }
}
