using UnityEngine;

/// <summary>
/// Makes party members follow the player in the overworld with configurable distance.
/// Handles sprite direction based on relative position and manages walking animation state.
/// </summary>
public class MemberFollowAI : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private int speed;

    private float followDist;
    private int lastDirection = 1; // 1 = right, -1 = left
    private Animator followerAnim;
    private SpriteRenderer spriteRenderer;
    private const string IS_WALKING = "isWalking";

    /// <summary>
    /// Initializes the animator and sprite renderer components, and finds the player as the follow target.
    /// </summary>
    void Start()
    {
        followerAnim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        followTarget = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

    /// <summary>
    /// Updates the party member's position to follow the player at a set distance.
    /// Handles sprite direction flipping and walking animation based on distance from the target.
    /// Called during the physics update phase.
    /// </summary>

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, followTarget.position);

        if (distance > followDist)
        {
            followerAnim.SetBool(IS_WALKING, true);

            MoveTowardsTarget();
            HandleFlip();
        }
        else
        {
            followerAnim.SetBool(IS_WALKING, false);
        }
    }

    /// <summary>
    /// Moves the follower towards the target using fixed timestep.
    /// </summary>
    private void MoveTowardsTarget()
    {
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, followTarget.position, step);
    }

    /// <summary>
    /// Flips the sprite based on movement direction using scale.
    /// Avoids lighting issues caused by rotation.
    /// </summary>
    private void HandleFlip()
    {
        float directionX = followTarget.position.x - transform.position.x;
        if (Mathf.Abs(directionX) < 0.01f) return;

        int direction = directionX < 0 ? -1 : 1;
        if (direction == lastDirection) return;

        lastDirection = direction;
        Vector3 scale = spriteRenderer.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        spriteRenderer.transform.localScale = scale;
    }

    /// <summary>
    /// Sets the distance this party member should maintain from the follow target (player).
    /// </summary>
    /// <param name="followDistance">The desired distance to maintain from the player.</param>
    public void SetFollowDistance(float followDistance)
    {
        followDist = followDistance;
    }
}
