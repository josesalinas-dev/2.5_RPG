using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls player movement, animation, and battle transitions in the overworld.
/// Handles input through the new InputSystem, manages sprite direction, and saves position when transitioning to battles.
/// </summary>
public class PlayerController : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private SpriteRenderer playerSprite;

    private PlayerControls playerControls;
    private Rigidbody rb;
    private Vector3 movement;
    private PartyManager partyManager;

    private const string IS_WALKING = "isWalking";

    /// <summary>
    /// Initializes the player controls input system on awake.
    /// </summary>
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    /// <summary>
    /// Enables the player input controls when the script is enabled.
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// Disables the player input controls when the script is disabled.
    /// </summary>
    void OnDisable()
    {        
        playerControls.Disable();
    }

    /// <summary>
    /// Initializes the player's rigidbody and restores the saved position from the PartyManager.
    /// Called at the start of the scene.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        partyManager = FindFirstObjectByType<PartyManager>();
        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
    }

    /// <summary>
    /// Reads input from the player and updates movement direction and animation state.
    /// Handles sprite direction flipping based on horizontal input.
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        float x = playerControls.Player.Move.ReadValue<Vector2>().x;
        float z = playerControls.Player.Move.ReadValue<Vector2>().y;

        movement = new Vector3(x, 0, z).normalized;

        if (!playerAnim || !playerSprite) return;

        playerAnim.SetBool(IS_WALKING, movement != Vector3.zero);

        if (x != 0)
        {
            // If x > 0 (moving right), rotation is 0. 
            // If x < 0 (moving left), rotation is 180 on the Y axis.
            float targetYRotation = (x < 0) ? 180f : 0f;

            // Apply rotation to the sprite's transform
            playerSprite.transform.localRotation = Quaternion.Euler(0, targetYRotation, 0);
        }
    }

    /// <summary>
    /// Handles the player being hit by an enemy in the dungeon.
    /// Saves the player's position and transitions to the battle scene.
    /// </summary>
    public void ProcessHit()
    {
        partyManager.SetPosition(transform.position);
        SceneManager.LoadScene("BattleScene");
    }

    /// <summary>
    /// Applies the calculated movement velocity to the rigidbody during the physics update phase.
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Sets the animator and sprite renderer components for the player's visual representation.
    /// Called by CharacterManager when spawning party member visuals.
    /// </summary>
    /// <param name="animator">The animator component to use for animations.</param>
    /// <param name="spriteRenderer">The sprite renderer component to use for sprite rendering.</param>
    public void SetOverworldVisuals(Animator animator, SpriteRenderer spriteRenderer)
    {
        playerAnim = animator;
        playerSprite = spriteRenderer;
    }
}
