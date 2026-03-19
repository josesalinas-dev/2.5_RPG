using System.Collections;
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

    void Start()
    {
        CheckIfJoined();
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
        interactPrompt.SetActive(showPrompt);
    }

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
