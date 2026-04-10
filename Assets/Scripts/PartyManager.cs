<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton manager that handles party member data and persistence across scenes.
/// Tracks current party composition, saves/loads character stats, and manages player position between scenes.
/// Destroyed when returning to main menu.
/// </summary>
public class PartyManager : MonoBehaviour
{
    [SerializeField] private PartyMemberInfo[] allMembers;
    [SerializeField] private List<PartyMember> currentParty;
    [SerializeField] private PartyMemberInfo defaultPartyMember;

    private Vector3 playerPosition;
<<<<<<< HEAD
    [SerializeField] public Vector3 playerStartPosition = new Vector3(0f, 0f, 0f);

    private static PartyManager instance;

    /// <summary>
    /// Initializes the PartyManager singleton, sets up the default party member, and registers for scene load events.
    /// </summary>
=======
    public Vector3 playerStartPosition = new Vector3(40f, 0f, 14.5f);

    private static PartyManager instance;

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
<<<<<<< HEAD
=======

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
        AddMembertoPartyByName(defaultPartyMember.memberName);
        SceneManager.sceneLoaded += OnSceneLoaded; // Escuchar el cambio de escenas

    }

<<<<<<< HEAD
    /// <summary>
    /// Handles scene load events and destroys the PartyManager if the main menu is loaded.
    /// </summary>
    /// <param name="scene">The scene that was loaded.</param>
    /// <param name="mode">The scene load mode.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Destruir el objeto si se carga el menú principal o la escena final
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);  // Destruye este objeto si es la escena deseada
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// Unregisters the scene load event when this manager is destroyed.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Eliminar el evento cuando se destruya el objeto
    }

<<<<<<< HEAD
    /// <summary>
    /// Adds a new party member to the active party by searching for their data by name.
    /// Initializes the party member with default stats from their ScriptableObject definition.
    /// </summary>
    /// <param name="memberName">The name of the party member to add.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void AddMembertoPartyByName(string memberName)
    {
        for (int i = 0; i < allMembers.Length; i++)
        {
            if (allMembers[i].memberName == memberName)
            {
                PartyMember newPartyMember = new PartyMember();
                newPartyMember.memberName = allMembers[i].memberName;
                newPartyMember.level = allMembers[i].startingLevel;
                newPartyMember.currentHealth = allMembers[i].baseHealth;
                newPartyMember.maxHealth = newPartyMember.currentHealth;
                newPartyMember.strength = allMembers[i].baseStr;
                newPartyMember.initiative = allMembers[i].baseInitiative;
                newPartyMember.sprite = allMembers[i].memberHUDSprite;
                newPartyMember.memberBattleVisualPrefab = allMembers[i].memberBattleVisualPrefab;
                newPartyMember.memberOverworldVisualPrefab = allMembers[i].memberOverworldVisualPrefab;
                currentParty.Add(newPartyMember);
            }
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// Returns a list of currently alive party members (with health > 0).
    /// Used by the battle system to determine valid participants in combat.
    /// </summary>
    /// <returns>A list of party members with positive health.</returns>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public List<PartyMember> GetAliveParty()
    {
        List<PartyMember> aliveParty = new List<PartyMember>();
        aliveParty = currentParty;
        for (int i = 0; i < aliveParty.Count; i++)
        {
            if (aliveParty[i].currentHealth <= 0)
            {
                aliveParty.RemoveAt(i);
            }
        }
        return aliveParty;
    }

<<<<<<< HEAD
    /// <summary>
    /// Returns the current list of active party members.
    /// </summary>
    /// <returns>The list of party members currently in the party.</returns>
=======

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public List<PartyMember> GetCurrentParty()
    {
        return currentParty;
    }

<<<<<<< HEAD
    /// <summary>
    /// Updates the health value of a specific party member after a battle.
    /// </summary>
    /// <param name="partyMember">The index of the party member to update.</param>
    /// <param name="health">The new health value to set.</param>
=======

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void SaveHealth(int partyMember, int health)
    {
        currentParty[partyMember].currentHealth = health;
    }

<<<<<<< HEAD
    /// <summary>
    /// Sets the player's position, typically saved when transitioning to a battle.
    /// </summary>
    /// <param name="position">The position to save.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void SetPosition(Vector3 position)
    {
        playerPosition = position;
    }

<<<<<<< HEAD
    /// <summary>
    /// Retrieves the previously saved player position.
    /// Used to restore the player's location after exiting a battle.
    /// </summary>
    /// <returns>The saved player position.</returns>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public Vector3 GetPosition()
    {
        return playerPosition;
    }


}

[System.Serializable]
public class PartyMember
{
    public string memberName;
    public int level;
    public int currentHealth;
    public int maxHealth;
    public int strength;
    public int initiative;
    public int currExp;
    public int maxExp;
    public Sprite sprite;
    public GameObject memberBattleVisualPrefab;
    public GameObject memberOverworldVisualPrefab;
}