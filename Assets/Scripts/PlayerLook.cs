using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    // =================================================
    // Reference
    // =================================================
    [Header("Reference")]
    [SerializeField] Transform _player;
    [SerializeField] Transform _cameraPivot;
    [SerializeField] InputActionReference _lookAction;
   
    ScopeController _scopeController;

    // =================================================
    // 감도 설정
    // =================================================
    [Header("Setting")]
    [SerializeField] [Range(0f, 0.1f)] float sensitivity = 0.05f;

    // =================================================
    // 축
    // =================================================
    float _pitch;
    float _yaw;

    // =================================================
    // 감도 계산 / 시야 범위 제한
    // =================================================
    public void Look()
    {
        Vector2 mouseDelta = _lookAction.action.ReadValue<Vector2>();

        float curSensitivity = sensitivity;

        if (_scopeController.IsAiming)
        {
            float sensitivityMultiplier = _scopeController.CurrentFOV / _scopeController.MaxFOV;

            curSensitivity *= sensitivityMultiplier;
        }

        mouseDelta *= curSensitivity;

        // 상하(Pitch) 제한
        _pitch -= mouseDelta.y;
        _pitch = Mathf.Clamp(_pitch, -60f, 60f);

        // 좌우(Yaw) 제한
        _yaw += mouseDelta.x;
        _yaw = Mathf.Clamp(_yaw, -45f, 45f);

        _cameraPivot.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    // =================================================
    // 활성화
    // =================================================
    public void OnEnable()
    {
        _lookAction.action.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // =================================================
    // 비활성화
    // =================================================
    public void OnDisable()
    {
        _lookAction.action.Disable();
    }

    // =================================================
    // Start
    // =================================================
    void Awake()
    {
        _scopeController = GetComponent<ScopeController>();
    }

    // =================================================
    // Update
    // =================================================
    void Update()
    {
        Look();
    }
}
