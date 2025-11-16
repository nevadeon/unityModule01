using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPlayer : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference selectClaire;
    [SerializeField] private InputActionReference selectThomas;
    [SerializeField] private InputActionReference selectJohn;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference moveAction;


    [Header("GameObjects")]
    [SerializeField] private GameObject claire;
    [SerializeField] private GameObject thomas;
    [SerializeField] private GameObject john;
    [SerializeField] private FollowPlayer cameraObj;

    private PlayerController claireController;
    private PlayerController thomasController;
    private PlayerController johnController;

    private PlayerController selectedController;


    private void OnValidate()
    {
        if (
            selectClaire == null ||
            selectJohn == null ||
            selectThomas == null ||
            jumpAction == null ||
            moveAction == null
        ) { Debug.LogWarning("Input action reference not set"); }

        if (claire == null ||
            john == null ||
            thomas == null ||
            cameraObj == null
        ) { Debug.LogWarning("Game object reference not set"); }
    }

    private void Awake()
    {
        claireController = claire.GetComponent<PlayerController>();
        johnController = john.GetComponent<PlayerController>();
        thomasController = thomas.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        selectClaire.action.performed += ClaireSelected;
        selectJohn.action.performed += JohnSelected;
        selectThomas.action.performed += ThomasSelected;

        selectClaire.action.Enable();
        selectJohn.action.Enable();
        selectThomas.action.Enable();

        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        selectClaire.action.performed -= ClaireSelected;
        selectJohn.action.performed -= JohnSelected;
        selectThomas.action.performed -= ThomasSelected;

        selectClaire.action.Disable();
        selectJohn.action.Disable();
        selectThomas.action.Disable();

        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        if (selectedController == null) { return; }

        float input = moveAction.action.ReadValue<float>();
        bool hasJumped = jumpAction.action.WasPressedThisFrame();

        selectedController.UpdateWhenSelected(input, hasJumped);
    }

    private void ClaireSelected(InputAction.CallbackContext ctx)
    {
        selectedController = claireController;
        cameraObj.targetPlayer = claire;
    }

    private void JohnSelected(InputAction.CallbackContext ctx)
    {
        selectedController = johnController;
        cameraObj.targetPlayer = john;
    }

    private void ThomasSelected(InputAction.CallbackContext ctx)
    {
        selectedController = thomasController;
        cameraObj.targetPlayer = thomas;
    }

}
