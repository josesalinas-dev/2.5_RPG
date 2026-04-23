namespace RPGInterfaces
{
    /// <summary>
    /// Interface for audio management functionality.
    /// Provides methods for controlling volume levels and mute state.
    /// </summary>
    public interface IAudioManager
    {
        /// <summary>
        /// Sets the master volume level.
        /// </summary>
        /// <param name="volume">Volume level between 0 and 1.</param>
        void SetMasterVolume(float volume);

        /// <summary>
        /// Sets the music volume level.
        /// </summary>
        /// <param name="volume">Volume level between 0 and 1.</param>
        void SetMusicVolume(float volume);

        /// <summary>
        /// Sets the UI volume level.
        /// </summary>
        /// <param name="volume">Volume level between 0 and 1.</param>
        void SetUIVolume(float volume);

        /// <summary>
        /// Sets the SFX volume level.
        /// </summary>
        /// <param name="volume">Volume level between 0 and 1.</param>
        void SetSFXVolume(float volume);

        /// <summary>
        /// Gets the current master volume level.
        /// </summary>
        /// <returns>Volume level between 0 and 1.</returns>
        float GetMasterVolume();

        /// <summary>
        /// Gets the current music volume level.
        /// </summary>
        /// <returns>Volume level between 0 and 1.</returns>
        float GetMusicVolume();

        /// <summary>
        /// Gets the current UI volume level.
        /// </summary>
        /// <returns>Volume level between 0 and 1.</returns>
        float GetUIVolume();

        /// <summary>
        /// Gets the current SFX volume level.
        /// </summary>
        /// <returns>Volume level between 0 and 1.</returns>
        float GetSFXVolume();

        /// <summary>
        /// Sets the mute state for all audio.
        /// </summary>
        /// <param name="mute">True to mute, false to unmute.</param>
        void SetMuteState(bool mute);

        /// <summary>
        /// Gets the current mute state.
        /// </summary>
        /// <returns>True if muted, false otherwise.</returns>
        bool IsMuted();
    }
}