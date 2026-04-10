using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private EnemySimpleAI enemy;
    private const string PLAYER_TAG = "Player";

    /// <summary>
    /// Caches a reference to the parent EnemySimpleAI component on awake.
    /// Used for triggering hit reactions and ending attack animations.
    /// </summary>
    void Awake()
    {
        // Cacheamos referencia al padre
        if (enemy == null)
        {
            enemy = GetComponentInParent<EnemySimpleAI>();
        }
    }

    /// <summary>
    /// Detects when the player collides with this hitbox and triggers a hit on the player.
    /// Called automatically by the physics system when a trigger collision occurs.
    /// </summary>
    /// <param name="other">The collider that entered this trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            enemy.HitPlayer();
        }
    }

    /// <summary>
    /// Called by animation events at the end of an attack animation.
    /// Signals the enemy AI to end its current attack and transition to the next state.
    /// </summary>
    public void EndAttack()
    {
        enemy.EndAttack();
    }
}
