<<<<<<< HEAD
using UnityEngine;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20

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

<<<<<<< HEAD
    /// <summary>
    /// Initializes the animator and sprite renderer components, and finds the player as the follow target.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    void Start()
    {
        followerAnim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
<<<<<<< HEAD
        followTarget = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

    /// <summary>
    /// Updates the party member's position to follow the player at a set distance.
    /// Handles sprite direction flipping and walking animation based on distance from the target.
    /// Called during the physics update phase.
    /// </summary>
=======

        followTarget = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
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

<<<<<<< HEAD
    /// <summary>
    /// Sets the distance this party member should maintain from the follow target (player).
    /// </summary>
    /// <param name="followDistance">The desired distance to maintain from the player.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void SetFollowDistance(float followDistance)
    {
        followDist = followDistance;
    }
}
