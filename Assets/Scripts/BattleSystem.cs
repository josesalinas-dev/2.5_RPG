using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the turn-based battle system including turn order, action execution, and win/loss conditions.
/// Spawns party members and enemies, manages battle UI, and handles damage/death calculations.
/// Supports attack and run actions with a 50% escape chance.
/// </summary>
public class BattleSystem : MonoBehaviour
{
    private enum BattleState { Start, Selection, Battle, Won, Lost, Run }

    [Header("Battle State")]
    [SerializeField] private BattleState State;

    [Header("SpawnPoints")]
    [SerializeField] private Transform[] partySpawnPoints;
    [SerializeField] private Transform[] enemySpawnPoints;

    [Header("Battlers")]
    [SerializeField] private List<BattleEntities> allBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> playerBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> enemyBattlers = new List<BattleEntities>();

    [Header("UI")]
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject enemySelectionMenu;
    [SerializeField] private GameObject[] enemySelectionButtons;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private GameObject battleInfoPanel;
    [SerializeField] private TextMeshProUGUI battleInfoText;

    private PartyManager partyManager;
    private EnemyManager enemyManager;
    private int currentPartyHero;

    private const string ACTION_MESSAGE = "'s Action: ";
    private const string WIN_MESSAGE = "Your Party Won The Battle!!!";
    private const string LOST_MESSAGE = "YOUR PARTY HAS BEEN DEFEATED!!!";
    private const string SUCCESSFULLY_RAN_AWAY_MESSAGE = "YOU HAVE RUN AWAY";
    private const string FAIL_RAN_AWAY_MESSAGE = "PARTY FAIL TO RUN";
    private const string SCENE_NAME = "RedDungeonLVL";
    private const int TURN_DURATION = 2;
    private const int RUN_CHANCE = 50;

    void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();

        CreatePartyEntities();
        CreateEnemyEntities();
        ShowBattleMenu();
        DetermineBattleOrder();
    }

    /// <summary>
    /// Creates BattleEntity instances for all alive party members and instantiates their visual representations.
    /// Initializes health bars and adds them to the party battlers list.
    /// </summary>
    private void CreatePartyEntities()
    {
        List<PartyMember> currentParty = new List<PartyMember>();
        currentParty = partyManager.GetAliveParty();

        for (int i = 0; i < currentParty.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();
            tempEntity.SetEntityValues(currentParty[i].memberName, currentParty[i].currentHealth, currentParty[i].maxHealth, currentParty[i].strength, currentParty[i].initiative, currentParty[i].level, true);

            BattleVisuals tempBattleVisual = Instantiate(currentParty[i].memberBattleVisualPrefab, partySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            tempBattleVisual.SetStartingValues(currentParty[i].currentHealth, currentParty[i].maxHealth, currentParty[i].level);
            tempEntity.BattleVisuals = tempBattleVisual;

            allBattlers.Add(tempEntity);
            playerBattlers.Add(tempEntity);
        }
    }

    /// <summary>
    /// Main battle coroutine that executes all battlers' actions in turn order.
    /// Processes attacks or run actions, removes defeated battlers, and checks for win/loss conditions.
    /// Repeats until the battle state changes or one side is defeated.
    /// </summary>
    private IEnumerator BattleRoutine()
    {
        enemySelectionMenu.SetActive(false);
        State = BattleState.Battle;
        battleInfoPanel.SetActive(true);
        enemyManager.hasWonBattle = false;

        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (State == BattleState.Battle && allBattlers[i].CurrentHealth > 0)
            {
                switch (allBattlers[i].BattleAction)
                {
                    case BattleEntities.Action.Attack:
                        yield return StartCoroutine(AttackRoutine(i));
                        break;
                    case BattleEntities.Action.Run:
                        yield return StartCoroutine(RunRoutine());
                        break;
                    default:
                        Debug.LogError("Error - incorrect battle action");
                        break;
                }

            }
        }

        RemoveDeadBattlers();

        if (State == BattleState.Battle)
        {
            battleInfoPanel.SetActive(false);
            currentPartyHero = 0;
            ShowBattleMenu();
        }

        yield return null;
    }

    /// <summary>
    /// Handles the attack action for both player and enemy combatants.
    /// For player attacks: targets are already set by SelectEnemy().
    /// For enemy attacks: randomly selects a target from living party members.
    /// Applies damage, updates UI, and checks for defeated targets.
    /// </summary>
    /// <param name="i">The index of the attacker in the allBattlers list.</param>
    private IEnumerator AttackRoutine(int i)
    {
        if (allBattlers[i].IsPlayer)
        {
            BattleEntities currAttacker = allBattlers[i];
            if (allBattlers[currAttacker.Target].CurrentHealth <= 0)
            {
                currAttacker.SetTarget(GetRandomEnemy());
            }
            BattleEntities currTarget = allBattlers[currAttacker.Target];

            AttackAction(currAttacker, currTarget);
            yield return new WaitForSeconds(TURN_DURATION);

            if (currTarget.CurrentHealth <= 0)
            {
                battleInfoText.text = string.Format("{0} defeated {1}", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION);
                enemyBattlers.Remove(currTarget);
                if (enemyBattlers.Count <= 0)
                {
                    State = BattleState.Won;
                    enemyManager.hasWonBattle = true;
                    battleInfoText.text = WIN_MESSAGE;
                    SceneManager.LoadScene(SCENE_NAME);
                }
            }
        }
        if (i < allBattlers.Count && allBattlers[i].IsPlayer == false)
        {
            BattleEntities currAttacker = allBattlers[i];
            currAttacker.SetTarget(GetRandomPartyMember());
            BattleEntities currTarget = allBattlers[currAttacker.Target];
            AttackAction(currAttacker, currTarget);
            yield return new WaitForSeconds(TURN_DURATION);
            if (currTarget.CurrentHealth <= 0)
            {
                battleInfoText.text = string.Format("{0} defeated {1}", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION);
                playerBattlers.Remove(currTarget);
                if (playerBattlers.Count <= 0)
                {
                    State = BattleState.Lost;
                    battleInfoText.text = LOST_MESSAGE;
                    yield return new WaitForSeconds(TURN_DURATION);
                    GameOver();
                }
            }
        }
    }

    /// <summary>
    /// Displays the game over screen when the party has been defeated.
    /// </summary>
    private void GameOver()
    {        
        battleInfoPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Handles the run/escape action during battle.
    /// Gives the party a 50% chance to escape. If successful, returns to the dungeon scene.
    /// If unsuccessful, the party loses their turn but can try again next round.
    /// </summary>
    private IEnumerator RunRoutine()
    {
        if (State == BattleState.Battle)
        {
            if (Random.Range(1, 101) >= RUN_CHANCE)
            {
                partyManager.SetPosition(partyManager.playerStartPosition);
                battleInfoText.text = SUCCESSFULLY_RAN_AWAY_MESSAGE;
                State = BattleState.Run;
                allBattlers.Clear();
                yield return new WaitForSeconds(TURN_DURATION);
                SceneManager.LoadScene(SCENE_NAME);
                yield break;

            }
            else
            {
                battleInfoText.text = FAIL_RAN_AWAY_MESSAGE;
                yield return new WaitForSeconds(TURN_DURATION);
            }
        }
    }

    /// <summary>
    /// Removes all defeated battlers (health <= 0) from the main battlers list.
    /// Prevents defeated entities from acting and being targeted later in the battle.
    /// </summary>
    private void RemoveDeadBattlers()
    {
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].CurrentHealth <= 0)
            {
                allBattlers.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Creates BattleEntity instances for all enemies in the current encounter.
    /// Instantiates their visual representations and initializes them for battle.
    /// </summary>
    private void CreateEnemyEntities()
    {
        List<Enemy> currentEnemies = new List<Enemy>();
        currentEnemies = enemyManager.GetCurrentEnemies();

        for (int i = 0; i < currentEnemies.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();
            tempEntity.SetEntityValues(currentEnemies[i].enemyName, currentEnemies[i].currentHealth, currentEnemies[i].maxHealth, currentEnemies[i].strength, currentEnemies[i].initiative, currentEnemies[i].level, false);

            BattleVisuals tempBattleVisual = Instantiate(currentEnemies[i].enemyBattleVisuals, enemySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            tempBattleVisual.SetStartingValues(currentEnemies[i].maxHealth, currentEnemies[i].maxHealth, currentEnemies[i].level);
            tempEntity.BattleVisuals = tempBattleVisual;

            allBattlers.Add(tempEntity);
            enemyBattlers.Add(tempEntity);
        }
    }

    /// <summary>
    /// Displays the action selection menu for the current party member's turn.
    /// Updates the action text to show whose turn it is.
    /// </summary>
    public void ShowBattleMenu()
    {
        actionText.text = playerBattlers[currentPartyHero].Name + ACTION_MESSAGE;
        battleMenu.SetActive(true);
    }

    /// <summary>
    /// Shows the enemy selection menu allowing the player to choose an attack target.
    /// Displays buttons for each alive enemy.
    /// </summary>
    public void ShowEnemySelectionMenu()
    {
        battleMenu.SetActive(false);
        SetEnemySelectionButtons();
        enemySelectionMenu.SetActive(true);
    }

    /// <summary>
    /// Configures the enemy selection button display.
    /// Activates buttons for each alive enemy and updates their labels with enemy names.
    /// Deactivates unused buttons.
    /// </summary>
    private void SetEnemySelectionButtons()
    {
        for (int i = 0; i < enemySelectionButtons.Length; i++)
        {
            enemySelectionButtons[i].SetActive(false);
        }

        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySelectionButtons[j].SetActive(true);
            enemySelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
    }

    /// <summary>
    /// Processes a player's selection of an enemy target.
    /// Sets the attack target and action, then advances to the next party member or starts the battle routine.
    /// </summary>
    /// <param name="currentEnemy">The index of the selected enemy in the enemyBattlers list.</param>
    public void SelectEnemy(int currentEnemy)
    {
        BattleEntities currentPlayerEntity = playerBattlers[currentPartyHero];
        currentPlayerEntity.SetTarget(allBattlers.IndexOf(enemyBattlers[currentEnemy]));
        currentPlayerEntity.BattleAction = BattleEntities.Action.Attack;
        currentPartyHero++;

        if (currentPartyHero >= playerBattlers.Count)
        {
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false);
            ShowBattleMenu();
        }
    }

    /// <summary>
    /// Executes a single attack action between two combatants.
    /// Calculates damage, plays animations, updates health, and logs the combat message.
    /// </summary>
    /// <param name="currAttacker">The BattleEntity performing the attack.</param>
    /// <param name="currTarget">The BattleEntity receiving the attack.</param>
    private void AttackAction(BattleEntities currAttacker, BattleEntities currTarget)
    {
        int damage = currAttacker.Strength;// to do: getDamage()  function to increase damage according to lvl, stats or items
        currAttacker.BattleVisuals.PlayAttackAnimation();
        currTarget.CurrentHealth -= damage;
        currTarget.BattleVisuals.PlayHitAnimation();
        currTarget.UpdateUI();
        battleInfoText.text = string.Format("{0} Attacks {1} for {2} damage", currAttacker.Name, currTarget.Name, damage);
        SaveHealth();
    }

    /// <summary>
    /// Finds a random living party member from the allBattlers list.
    /// Used by enemies to randomly select attack targets.
    /// </summary>
    /// <returns>The index of a random alive party member.</returns>
    private int GetRandomPartyMember()
    {
        List<int> partyMembers = new List<int>();
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].IsPlayer == true && allBattlers[i].CurrentHealth > 0)
            {
                partyMembers.Add(i);
            }
        }

        return partyMembers[Random.Range(0, partyMembers.Count)];
    }

    /// <summary>
    /// Finds a random living enemy from the allBattlers list.
    /// Used by players to select attack targets when not explicitly chosen.
    /// </summary>
    /// <returns>The index of a random alive enemy.</returns>
    private int GetRandomEnemy()
    {
        List<int> enemyMembers = new List<int>();
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].IsPlayer == false && allBattlers[i].CurrentHealth > 0)
            {
                enemyMembers.Add(i);
            }
        }

        return enemyMembers[Random.Range(0, enemyMembers.Count)];
    }

    /// <summary>
    /// Saves the current health of all party members to the PartyManager for persistence across scenes.
    /// </summary>
    private void SaveHealth()
    {
        for (int i = 0; i < playerBattlers.Count; i++)
        {
            partyManager.SaveHealth(i, playerBattlers[i].CurrentHealth);
        }
    }

    /// <summary>
    /// Sorts all battlers by their initiative stat (highest first) to determine turn order.
    /// Entities with higher initiative act earlier in each battle round.
    /// </summary>
    private void DetermineBattleOrder()
    {
        allBattlers.Sort((bi1, bi2) => -bi1.Initiative.CompareTo(bi2.Initiative));
    }

    /// <summary>
    /// Processes a player's selection to run from the battle.
    /// Sets the run action and advances to the next hero or starts the battle routine.
    /// </summary>
    public void SelectRunAction()
    {
        State = BattleState.Selection;
        BattleEntities currentPlayerEntity = playerBattlers[currentPartyHero];
        currentPlayerEntity.BattleAction = BattleEntities.Action.Run;

        battleMenu.SetActive(false);
        currentPartyHero++;

        if (currentPartyHero >= playerBattlers.Count)
        {
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false);
            ShowBattleMenu();
        }

    }

}

/// <summary>
/// Internal data class for BattleEntities representing targets and actions during combat.
/// </summary>
[System.Serializable]
public class BattleEntities
{
    /// <summary>
    /// Enum defining possible actions a battler can perform during their turn.
    /// </summary>
    public enum Action { Attack, Run }
    
    public Action BattleAction;

    public string Name;
    public int Level;
    public int CurrentHealth;
    public int MaxHealth;
    public int Strength;
    public int Initiative;
    public bool IsPlayer;
    public BattleVisuals BattleVisuals;
    public int Target;

    /// <summary>
    /// Initializes all stats for a battle entity from party member or enemy data.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="currentHealth">The current health of the entity.</param>
    /// <param name="maxHealth">The maximum health of the entity.</param>
    /// <param name="strength">The attack power/damage of the entity.</param>
    /// <param name="initiative">The turn order priority of the entity.</param>
    /// <param name="level">The level of the entity.</param>
    /// <param name="isPlayer">Whether this entity belongs to the player's party.</param>
    public void SetEntityValues(string name, int currentHealth, int maxHealth, int strength, int initiative, int level, bool isPlayer)
    {
        Name = name;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        Strength = strength;
        Initiative = initiative;
        IsPlayer = isPlayer;
        Level = level;
    }

    /// <summary>
    /// Sets the target index for this entity's next action in the allBattlers list.
    /// </summary>
    /// <param name="target">The index of the target entity in the allBattlers list.</param>
    public void SetTarget(int target)
    {
        Target = target;
    }

    /// <summary>
    /// Updates the visual health bar for this entity after damage is taken or health changes.
    /// </summary>
    public void UpdateUI()
    {
        BattleVisuals.ChangeHealth(CurrentHealth);
    }
}