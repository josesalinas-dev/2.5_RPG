using UnityEngine;

/// <summary>
/// Simple utility class for managing object animations.
/// Currently provides method to disable/hide objects after animation completion.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public void DisableThisObject(){
        gameObject.SetActive(false);
    }
}
