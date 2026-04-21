using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles visual feedback and animations for battle characters.
/// Manages health bars, plays attack/hit/death animations, and cleans up defeated character visuals.
/// </summary>
public class BattleVisuals : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private int currentHealth;
    private int maxHealth;
    private Animator animator;

    private const string IS_ATTACKING_PARAM = "isAttacking";
    private const string IS_DEATH_PARAM = "isDeath";
    private const string IS_HIT_PARAM = "isHit";
    
    /// <summary>
    /// Initializes the animator component reference on awake.
    /// </summary>
    void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Sets the initial health values and updates the health bar display.
    /// Called when the battle entity is created.
    /// </summary>
    /// <param name="currentHealth">The current health value of the character.</param>
    /// <param name="maxHealth">The maximum health value of the character.</param>
    /// <param name="level">The level of the character (currently unused).</param>
    public void SetStartingValues(int currentHealth, int maxHealth, int level)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the health bar slider to reflect the current health value.
    /// </summary>
    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    /// <summary>
    /// Updates the current health value and triggers death animations if health reaches zero.
    /// Destroys the game object after 5 seconds if defeated.
    /// </summary>
    /// <param name="currentHealth">The new health value to set.</param>
    public void ChangeHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 5f);
        }
        UpdateHealthBar();
    }

    /// <summary>
    /// Plays the attack animation for this character.
    /// Called when the character performs an attack action.
    /// </summary>
    public void PlayAttackAnimation()
    {
        animator.SetTrigger(IS_ATTACKING_PARAM);
    }

    /// <summary>
    /// Plays the hit/damage reaction animation for this character.
    /// Called when the character takes damage.
    /// </summary>
    public void PlayHitAnimation()
    {
        animator.SetTrigger(IS_HIT_PARAM);
    }

    /// <summary>
    /// Plays the death animation for this character.
    /// Called when the character's health reaches zero.
    /// </summary>
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(IS_DEATH_PARAM);
    }
}
