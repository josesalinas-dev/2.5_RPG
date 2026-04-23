namespace RPGInterfaces
{
    /// <summary>
    /// Interface for game management functionality.
    /// Provides methods for scene loading, pause/resume, and game quit.
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Loads a new scene by name and resumes normal game time if paused.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        void LoadScene(string sceneName);

        /// <summary>
        /// Pauses the game by setting Time.timeScale to 0 and displays the pause menu.
        /// </summary>
        void PauseGame();

        /// <summary>
        /// Resumes the game by setting Time.timeScale to 1 and hides the pause menu.
        /// </summary>
        void ResumeGame();

        /// <summary>
        /// Toggles the pause state between paused and resumed.
        /// </summary>
        void TogglePause();

        /// <summary>
        /// Closes the application or stops play mode in the editor.
        /// </summary>
        void QuitGame();
    }
}