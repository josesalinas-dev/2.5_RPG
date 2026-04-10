<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages NPC recruitment and party member visuals in the overworld.
/// Handles interaction detection with joinable NPCs, spawns party member visuals following the player, and updates the HUD.
/// </summary>
public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject joinPopUp;
    [SerializeField] private GameObject AvatarsHUD;
    [SerializeField] private TextMeshProUGUI joinPopUpText;
    private PartyManager partyManager;

    private bool infrontOfPartyMember;
    private GameObject joinableMember;
    private PlayerControls playerControls;
    private List<GameObject> overWorldCharacters = new List<GameObject>();

    private const string NPC_JOINABLE_TAG = "NPCJoinable";
    private const string PARTY_JOINED_MESSAGE = " Joined The Party!";

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
    /// Initializes the character manager by setting up input listeners, restoring player position, and spawning party member visuals.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void Start()
    {
        playerControls.Player.Interact.performed += _ => Interact();
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
        SpawnOverworldMembers();
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
    /// Disables the player input controls when the script is disabled.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnDisable()
    {
        playerControls.Disable();
    }

<<<<<<< HEAD
    /// <summary>
    /// Handles player interaction with nearby joinable NPCs.
    /// Recruits the NPC to the party and updates visuals if the player is in front of them.
    /// </summary>
    private void Interact()
    {
        if (joinableMember == null)
        {
            Debug.Log("joinableMember is null");
            return;
        }
=======
    private void Interact()
    {
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
        if (infrontOfPartyMember && joinableMember != null)
        {
            JoinMember(joinableMember.GetComponent<JoinableCharacterScript>().membertoJoin);
            infrontOfPartyMember = false;
            joinableMember = null;
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// Adds a new party member to the active party and updates the party visuals in the overworld.
    /// Displays a message confirming the party member has joined.
    /// </summary>
    /// <param name="partyMember">The PartyMemberInfo of the character to join the party.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void JoinMember(PartyMemberInfo partyMember)
    {
        GameObject.FindFirstObjectByType<PartyManager>().AddMembertoPartyByName(partyMember.memberName);
        joinableMember.GetComponent<JoinableCharacterScript>().CheckIfJoined();
        joinPopUp.SetActive(true);
        joinPopUpText.text = partyMember.memberName + PARTY_JOINED_MESSAGE;
        SpawnOverworldMembers();
    }

<<<<<<< HEAD
    /// <summary>
    /// Instantiates and positions all current party members in the overworld.
    /// The first member is the player themselves, and subsequent members follow behind using MemberFollowAI.
    /// Updates the HUD avatar display as well.
    /// </summary>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void SpawnOverworldMembers()
    {
        for (int i = 0; i < overWorldCharacters.Count; i++)
        {
            Destroy(overWorldCharacters[i]);
        }
        overWorldCharacters.Clear();
        List<PartyMember> currentParty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();
<<<<<<< HEAD
        if (AvatarsHUD != null)
        {
            var overworldVisuals = AvatarsHUD.GetComponent<OverworldVisuals>();
            if (overworldVisuals != null)
            {
                overworldVisuals.UpdateOverworldVisuals();
            }
        }
=======
        AvatarsHUD.GetComponent<OverworldVisuals>().UpdateOverworldVisuals();
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
        for (int i = 0; i < currentParty.Count; i++)
        {
            if (i == 0)
            {
                GameObject player = gameObject;
<<<<<<< HEAD
                GameObject playerVisual = Instantiate(
                    currentParty[i].memberOverworldVisualPrefab,
                    player.transform);
                playerVisual.transform.localPosition = Vector3.zero;
=======
                GameObject playerVisual = Instantiate(currentParty[i].memberOverworldVisualPrefab, player.transform.position, Quaternion.identity);
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
                playerVisual.transform.SetParent(player.transform);
                player.GetComponent<PlayerController>().SetOverworldVisuals(playerVisual.GetComponent<Animator>(), playerVisual.GetComponent<SpriteRenderer>());
                playerVisual.GetComponent<MemberFollowAI>().enabled = false;
                overWorldCharacters.Add(playerVisual);
            }
            else
            {
                Vector3 positionToSpawn = transform.position;
                positionToSpawn.x -= i;
                GameObject tempFollower = Instantiate(currentParty[i].memberOverworldVisualPrefab, positionToSpawn, Quaternion.identity);
                tempFollower.GetComponent<MemberFollowAI>().SetFollowDistance(i + 1.5f);
                overWorldCharacters.Add(tempFollower);
            }
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// Detects when the player enters a trigger collider with a joinable NPC.
    /// Shows an interaction prompt for the NPC.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
=======


>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            infrontOfPartyMember = true;
            joinableMember = other.gameObject;
            joinableMember.GetComponent<JoinableCharacterScript>().ShowInteractPrompt(infrontOfPartyMember);
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// Detects when the player exits a trigger collider with a joinable NPC.
    /// Hides the interaction prompt for the NPC.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
=======
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            infrontOfPartyMember = false;
<<<<<<< HEAD
            if (joinableMember != null)
            {
                var joinScript = joinableMember.GetComponent<JoinableCharacterScript>();
                if (joinScript != null)
                {
                    joinScript.ShowInteractPrompt(false);
                }
            }
=======
            joinableMember.GetComponent<JoinableCharacterScript>().ShowInteractPrompt(infrontOfPartyMember);
>>>>>>> c3bb495faa8b085aaa317109203126d7e8cbce20
            joinableMember = null;
        }
    }
}
