using UnityEngine;
using UnityEngine.Audio;
using RPGInterfaces;

/// <summary>
/// Singleton manager for handling audio settings and mixer control.
/// Manages master, music, UI, and SFX volume levels with persistence via PlayerPrefs.
/// Supports mute functionality and automatic volume restoration.
/// Implements IAudioManager interface for dependency injection.
/// </summary>
/// <remarks>
/// Responsabilidades: Gestión de volúmenes de audio, persistencia de configuración, control de mute.
/// Patrón: Singleton.
/// </remarks>
public class AudioManager : MonoBehaviour, IAudioManager
{
    /// <summary>
    /// Singleton instance of the AudioManager, accessible globally.
    /// Ensures only one AudioManager exists in the scene.
    /// </summary>
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    /// <summary>
    /// Reference to the Unity AudioMixer for controlling volume levels.
    /// Exposed parameters: MasterVolume, MusicVolume, UIVolume, SFXVolume.
    /// </summary>
    [SerializeField] private AudioMixer audioMixer;

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string UI_VOLUME_KEY = "UIVolume";
    private const string AUDIO_MUTED_KEY = "AudioMuted";
    private const float MUTE_DB = -80f;

    private float cachedMasterVolume = 0.3f;
    private float cachedMusicVolume = 0.3f;
    private float cachedUIVolume = 0.3f;
    private float cachedSFXVolume = 0.3f;

    private bool cachedMuted = false;

    /// <summary>
    /// Initializes the singleton instance and ensures persistence across scenes.
    /// Loads saved audio state from PlayerPrefs.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAudioState();
    }

    /// <summary>
    /// Applies the loaded audio state to the AudioMixer after initialization.
    /// </summary>
    private void Start()
    {
        ApplyAudioState();
    }

    /// <summary>
    /// Loads audio volume and mute settings from PlayerPrefs.
    /// Restores cached values for master, music, UI, and SFX volumes, plus mute state.
    /// </summary>
    private void LoadAudioState()
    {
        cachedMasterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 0.3f);
        cachedMusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.3f);
        cachedUIVolume = PlayerPrefs.GetFloat(UI_VOLUME_KEY, 0.3f);
        cachedSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.3f);
        cachedMuted = PlayerPrefs.GetInt(AUDIO_MUTED_KEY, 0) == 1;
    }

    /// <summary>
    /// Applies cached volume and mute settings to the AudioMixer.
    /// Converts linear volume to decibels and sets exposed parameters.
    /// </summary>
    private void ApplyAudioState()
    {
        float masterDb = cachedMuted
            ? MUTE_DB
            : ConvertLinearToDecibels(cachedMasterVolume);

        audioMixer.SetFloat("MasterVolume", masterDb);
        audioMixer.SetFloat("MusicVolume", ConvertLinearToDecibels(cachedMusicVolume));
        audioMixer.SetFloat("UIVolume", ConvertLinearToDecibels(cachedUIVolume));
        audioMixer.SetFloat("SFXVolume", ConvertLinearToDecibels(cachedSFXVolume));
    }

    /// <summary>
    /// Converts linear volume (0-1) to decibel scale for AudioMixer.
    /// Clamps input to valid range and uses logarithmic conversion.
    /// </summary>
    /// <param name="linearVolume">Linear volume value between 0 and 1.</param>
    /// <returns>Decibel value for AudioMixer parameter.</returns>
    private static float ConvertLinearToDecibels(float linearVolume)
    {
        linearVolume = Mathf.Clamp01(linearVolume);
        return Mathf.Log10(Mathf.Max(linearVolume, 0.0001f)) * 20f;
    }

    /// <summary>
    /// Sets the master volume level and persists the setting.
    /// If not muted, immediately applies the volume to the AudioMixer.
    /// </summary>
    /// <param name="volume">New master volume level (0-1).</param>
    public void SetMasterVolume(float volume)
    {
        cachedMasterVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, cachedMasterVolume);

        if (!cachedMuted)
        {
            float db = ConvertLinearToDecibels(cachedMasterVolume);
            audioMixer.SetFloat("MasterVolume", db);
        }
    }

    /// <summary>
    /// Sets the music volume level and persists the setting.
    /// Immediately applies the volume to the AudioMixer.
    /// </summary>
    /// <param name="volume">New music volume level (0-1).</param>
    public void SetMusicVolume(float volume)
    {
        cachedMusicVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, cachedMusicVolume);
        audioMixer.SetFloat("MusicVolume", ConvertLinearToDecibels(cachedMusicVolume));
    }

    /// <summary>
    /// Sets the UI volume level and persists the setting.
    /// Immediately applies the volume to the AudioMixer.
    /// </summary>
    /// <param name="volume">New UI volume level (0-1).</param>
    public void SetUIVolume(float volume)
    {
        cachedUIVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(UI_VOLUME_KEY, cachedUIVolume);
        audioMixer.SetFloat("UIVolume", ConvertLinearToDecibels(cachedUIVolume));
    }

    /// <summary>
    /// Sets the SFX volume level and persists the setting.
    /// Immediately applies the volume to the AudioMixer.
    /// </summary>
    /// <param name="volume">New SFX volume level (0-1).</param>
    public void SetSFXVolume(float volume)
    {
        cachedSFXVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, cachedSFXVolume);
        audioMixer.SetFloat("SFXVolume", ConvertLinearToDecibels(cachedSFXVolume));
    }
    
    public float GetMasterVolume() => cachedMasterVolume;
    
    public float GetMusicVolume() => cachedMusicVolume;
    
    public float GetUIVolume() => cachedUIVolume;
    
    public float GetSFXVolume() => cachedSFXVolume;

    /// <summary>
    /// Toggles the mute state for all audio.
    /// Persists the setting and applies mute by setting master volume to -80dB.
    /// </summary>
    /// <param name="mute">True to mute audio, false to unmute.</param>
    public void SetMuteState(bool mute)
    {
        cachedMuted = mute;
        PlayerPrefs.SetInt(AUDIO_MUTED_KEY, mute ? 1 : 0);

        float masterDb = mute
            ? MUTE_DB
            : ConvertLinearToDecibels(cachedMasterVolume);

        audioMixer.SetFloat("MasterVolume", masterDb);
    }

    /// <summary>
    /// Gets the current mute state.
    /// </summary>
    /// <returns>True if audio is muted, false otherwise.</returns>
    public bool IsMuted() => cachedMuted;
}