using UnityEngine;

/// <summary>
/// ScriptableObject that defines party member data (stats, visuals, prefabs).
/// Used to create reusable party member templates in the asset pipeline.
/// </summary>
[CreateAssetMenu(menuName = "NewPartyMember")]
public class PartyMemberInfo : ScriptableObject
{
    public string memberName;
    public int startingLevel;
    public int baseHealth;
    public int baseStr;
    public int baseInitiative;
    public GameObject memberBattleVisualPrefab;
    public GameObject memberOverworldVisualPrefab;
    public Sprite memberHUDSprite;
}
