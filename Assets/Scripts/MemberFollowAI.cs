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
    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, followTarget.position) > followDist)
        {
            followerAnim.SetBool(IS_WALKING, true);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, followTarget.position, step);
            if(followTarget.position.x - transform.position.x < 0)
            {
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0f, 0);
            }
        }
        else
        {
            followerAnim.SetBool(IS_WALKING, false);

        }
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
