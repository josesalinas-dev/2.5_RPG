using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes an NPC recruitable as a party member.
/// Displays interaction prompts and hides itself when the character has already joined the party.
/// </summary>
public class JoinableCharacterScript : MonoBehaviour
{
    public PartyMemberInfo membertoJoin;
    [SerializeField] private GameObject interactPrompt;

    /// <summary>
    /// Initializes the joinable character by checking if they've already joined the party.
    /// Hides the NPC if they're already part of the active party.
    /// </summary>
    void Start()
    {
        CheckIfJoined();
    }

    /// <summary>
    /// Shows or hides the interaction prompt UI element.
    /// Called when the player is near or far from this character.
    /// </summary>
    /// <param name="showPrompt">If true, shows the interaction prompt; if false, hides it.</param>
    public void ShowInteractPrompt(bool showPrompt)
    {
        interactPrompt.SetActive(showPrompt);
    }

    /// <summary>
    /// Checks if this character has already joined the party.
    /// Deactivates the NPC GameObject if they're already a member of the current party.
    /// </summary>
    public void CheckIfJoined()
    {
        List<PartyMember> currParty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();
        
        for (int i = 0; i < currParty.Count; i++)
        {
            if (currParty[i].memberName == membertoJoin.memberName)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
