using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Detects when the player reaches the goal and transitions to the win scene.
/// Simple trigger-based level completion handler.
/// </summary>
public class WinManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("WinScene");
        }
    }
}
