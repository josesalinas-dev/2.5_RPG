using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays party member avatars and health bars in the HUD during overworld exploration.
/// Updates visuals whenever party composition changes (member joins/leaves).
/// </summary>
public class OverworldVisuals : MonoBehaviour
{
    [SerializeField] private PartyManager partyManager;
    [SerializeField] private GameObject[] heroPanelHUD;  
    [SerializeField] private Image[] heroPortraits;
    [SerializeField] private TextMeshProUGUI[] heroNames;
    [SerializeField] private TextMeshProUGUI[] heroLevels;
    [SerializeField] private Slider[] herohealthBars;

    /// <summary>
    /// Initializes the OverworldVisuals by finding the PartyManager and updating the HUD display.
    /// Called at the start of the scene.
    /// </summary>
    private void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        UpdateOverworldVisuals();
    }

    /// <summary>
    /// Updates the party member UI elements (avatars, names, levels, health bars) displayed in the HUD.
    /// Activates or deactivates HUD panels based on the current party size.
    /// Should be called whenever party composition changes.
    /// </summary>
    public void UpdateOverworldVisuals()
    {
        int partyCount = partyManager.GetCurrentParty().Count;
        for (int i = 0; i < heroPanelHUD.Length; i++)
        {
            if (i < partyCount)
            {
                if (heroPanelHUD[i] != null)
                {
                    heroPanelHUD[i].SetActive(true);
                }
                var partyMember = partyManager.GetCurrentParty()[i];

                if (heroPortraits[i] != null)
                {
                    heroPortraits[i].sprite = partyMember.sprite;
                }
                              
                if (heroNames[i] != null)
                {
                    heroNames[i].text = partyMember.memberName;
                }
                
                if (heroLevels[i] != null)
                {
                    heroLevels[i].text = partyMember.level.ToString();
                }
                
                if (herohealthBars[i] != null)
                {
                    herohealthBars[i].maxValue = partyMember.maxHealth;
                    herohealthBars[i].value = partyMember.currentHealth;
                }
            }
            else
            {                
                if (heroPanelHUD[i] != null)
                {
                    heroPanelHUD[i].SetActive(false);
                }
            }
        }
    }
}
