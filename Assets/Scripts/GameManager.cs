using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game-wide functionality including pause/resume, scene loading, and application quit.
/// Uses the new InputSystem to detect pause input and controls Time.timeScale for pause mechanics.
/// </summary>
public class GameManager : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private GameObject pauseMenu;
    // Variable para saber si el juego está pausado
    private bool isPaused = false;

<<<<<<< HEAD
    /// <summary>
    /// Initializes the player controls input system on awake.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

<<<<<<< HEAD
    /// <summary>
    /// Enables the player input controls when the script is enabled.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnEnable()
    {
        playerControls.Enable();
    }

<<<<<<< HEAD
    /// <summary>
    /// Sets up the pause input listener if not in the main menu scene.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }

<<<<<<< HEAD
        playerControls.Player.Pause.performed += _ => PauseGame();
    }

    /// <summary>
    /// Loads a new scene by name and resumes normal game time if paused.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
=======
        playerControls.Player.PauseGame.performed += _ => PauseGame();
    }

    // Método para cargar una nueva escena
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void LoadScene(string sceneName)
    {
        // Cambia el estado de pausa
        if (isPaused) isPaused = false;
        // Reanuda el tiempo en el juego
        Time.timeScale = 1f;
        // Carga la escena especificada
        SceneManager.LoadScene(sceneName);
    }

<<<<<<< HEAD
    /// <summary>
    /// Pauses the game by setting Time.timeScale to 0 and displays the pause menu.
    /// </summary>
=======
    // Método para pausar el juego
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void PauseGame()
    {
        // Cambia el estado de pausa
        isPaused = true;
        // Detiene el tiempo en el juego
        Time.timeScale = 0f;
        if (pauseMenu) pauseMenu.SetActive(true);
    }

<<<<<<< HEAD
    /// <summary>
    /// Resumes the game by setting Time.timeScale to 1 and hides the pause menu.
    /// </summary>
=======
    // Método para despausar el juego
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void ResumeGame()
    {
        // Cambia el estado de pausa
        isPaused = false;
        // Reanuda el tiempo en el juego
        Time.timeScale = 1f;
<<<<<<< HEAD
        if (pauseMenu) pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Closes the application or stops play mode in the editor.
    /// In the Unity editor, this stops play mode. In a build, it quits the application.
    /// </summary>
=======
    }

    // Método para cerrar el juego
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void QuitGame()
    {
        // Si estamos en el editor, solo detiene la ejecución
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Cierra la aplicación en el build
            Application.Quit();
#endif
    }

<<<<<<< HEAD
    /// <summary>
    /// Toggles the pause state between paused and resumed.
    /// Calls either ResumeGame or PauseGame depending on the current state.
    /// </summary>
=======
    // Método para alternar entre pausar y despausar el juego
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    public void TogglePause()
    {
        // Si el juego está pausado, lo despausamos
        if (isPaused)
        {
            ResumeGame();
        }
        // Si el juego no está pausado, lo pausamos
        else
        {
            PauseGame();
        }
    }
}
