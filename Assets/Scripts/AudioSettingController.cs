using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI controller for audio settings, connecting sliders and toggles to AudioManager.
/// Initializes UI elements with current values and sets up event listeners.
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

    /// <summary>
    /// Initializes the audio UI controls by setting up sliders and mute toggle.
    /// Checks for AudioManager instance and configures event listeners.
    /// </summary>
    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager Instance no encontrado. Asegúrate de que AudioManager existe en la escena.");
            return;
        }

        InitializeSlider(masterSlider, AudioManager.Instance.SetMasterVolume, AudioManager.Instance.GetMasterVolume);
        InitializeSlider(musicSlider, AudioManager.Instance.SetMusicVolume, AudioManager.Instance.GetMusicVolume);
        InitializeSlider(uiSlider, AudioManager.Instance.SetUIVolume, AudioManager.Instance.GetUIVolume);
        InitializeSlider(sfxSlider, AudioManager.Instance.SetSFXVolume, AudioManager.Instance.GetSFXVolume);

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
        bool isMuted = AudioManager.Instance.IsMuted();        
        muteToggle.SetIsOnWithoutNotify(!isMuted);
        muteToggle.onValueChanged.AddListener(toggleIsOn => AudioManager.Instance.SetMuteState(!toggleIsOn));
    }
}