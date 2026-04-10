using UnityEngine;

/// <summary>
/// Simple utility class for managing object animations.
/// Currently provides method to disable/hide objects after animation completion.
/// </summary>
public class AnimationManager : MonoBehaviour
{
<<<<<<< HEAD
    /// <summary>
    /// Disables this GameObject, typically called at the end of an animation sequence via an animation event.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void DisableThisObject(){
        gameObject.SetActive(false);
    }
}
