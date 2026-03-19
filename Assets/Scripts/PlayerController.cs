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

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        partyManager = FindFirstObjectByType<PartyManager>();
        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
    }

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

    public void ProcessHit()
    {
        partyManager.SetPosition(transform.position);
        SceneManager.LoadScene("BattleScene");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    public void SetOverworldVisuals(Animator animator, SpriteRenderer spriteRenderer)
    {
        playerAnim = animator;
        playerSprite = spriteRenderer;
    }
}
