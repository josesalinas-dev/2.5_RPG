using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Detects when the player reaches the goal and transitions to the win scene.
/// Simple trigger-based level completion handler.
/// </summary>
public class WinManager : MonoBehaviour
{
    /// <summary>
    /// Detects when the player enters the win zone trigger.
    /// Loads the win scene if the player collides with this trigger.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("WinScene");
        }
    }
}
