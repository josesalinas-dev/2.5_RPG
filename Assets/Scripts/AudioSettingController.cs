using UnityEngine;
using UnityEngine.UI;
using RPGInterfaces;

/// <summary>
/// UI controller for audio settings, connecting sliders and toggles to AudioManager.
/// Initializes UI elements with current values and sets up event listeners.
/// Uses dependency injection via IAudioManager interface.
/// </summary>
/// <remarks>
/// Responsabilidades: Vinculación de UI de audio con AudioManager, inicialización de controles.
/// Patrón: Ninguno específico.
/// </remarks>
public class AudioSettingController : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider uiSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Mute Toggle")]
    [SerializeField] private Toggle muteToggle;

    [Header("Dependencies")]
    [SerializeField] private AudioManager audioManagerImpl;
    private IAudioManager audioManager;

    /// <summary>
    /// Initializes the audio UI controls by setting up sliders and mute toggle.
    /// Checks for injected IAudioManager and configures event listeners.
    /// </summary>
    private void Start()
    {
        if (audioManagerImpl == null)
        {
            audioManagerImpl = FindFirstObjectByType<AudioManager>();
        }

        if (audioManagerImpl == null)
        {
            Debug.LogError("AudioManager no encontrado en la escena ni asignado en el Inspector.");
            return;
        }
 
        audioManager = audioManagerImpl;

        InitializeSlider(masterSlider, audioManager.SetMasterVolume, audioManager.GetMasterVolume);
        InitializeSlider(musicSlider, audioManager.SetMusicVolume, audioManager.GetMusicVolume);
        InitializeSlider(uiSlider, audioManager.SetUIVolume, audioManager.GetUIVolume);
        InitializeSlider(sfxSlider, audioManager.SetSFXVolume, audioManager.GetSFXVolume);

        InitializeMuteToggle();
    }

    /// <summary>
    /// Initializes a volume slider by setting its value and attaching the change callback.
    /// Removes existing listeners to prevent duplicates.
    /// </summary>
    /// <param name="slider">The slider to initialize.</param>
    /// <param name="callback">The action to call when slider value changes.</param>
    /// <param name="getValue">Function to get the current volume value.</param>
    private void InitializeSlider(Slider slider, UnityEngine.Events.UnityAction<float> callback, System.Func<float> getValue)
    {
        if (slider == null)
            return;

        slider.onValueChanged.RemoveAllListeners();
        slider.SetValueWithoutNotify(getValue());
        slider.onValueChanged.AddListener(callback);
    }

    /// <summary>
    /// Initializes the mute toggle by setting its state and attaching the change callback.
    /// The toggle is inverted (on = not muted) for better UX.
    /// </summary>
    private void InitializeMuteToggle()
    {
        if (muteToggle == null)
            return;

        muteToggle.onValueChanged.RemoveAllListeners();
        bool isMuted = audioManager.IsMuted();        
        muteToggle.SetIsOnWithoutNotify(!isMuted);
        muteToggle.onValueChanged.AddListener(toggleIsOn => audioManager.SetMuteState(!toggleIsOn));
    }
}