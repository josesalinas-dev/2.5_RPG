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
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetStartingValues(int currentHealth, int maxHealth, int level)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        UpdateHealthBar();
    }    

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void ChangeHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 1.5f);
        }
        UpdateHealthBar();
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger(IS_ATTACKING_PARAM);
    }

    public void PlayHitAnimation()
    {
        animator.SetTrigger(IS_HIT_PARAM);
    }
    
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(IS_DEATH_PARAM);
    }
}
