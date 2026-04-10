using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Detects when the player reaches the goal and transitions to the win scene.
/// Simple trigger-based level completion handler.
/// </summary>
public class WinManager : MonoBehaviour
{
<<<<<<< HEAD
    /// <summary>
    /// Detects when the player enters the win zone trigger.
    /// Loads the win scene if the player collides with this trigger.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("WinScene");
        }
    }
}
