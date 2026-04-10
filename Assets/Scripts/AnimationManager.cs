using UnityEngine;

/// <summary>
/// Simple utility class for managing object animations.
/// Currently provides method to disable/hide objects after animation completion.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    /// <summary>
    /// Disables this GameObject, typically called at the end of an animation sequence via an animation event.
    /// </summary>
    public void DisableThisObject(){
        gameObject.SetActive(false);
    }
}
