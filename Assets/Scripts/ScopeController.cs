using UnityEngine;
using UnityEngine.InputSystem;

public class ScopeController : MonoBehaviour
{
    // =================================================
    // Reference
    // =================================================
    [Header("Reference")]
    [SerializeField] InputActionReference _aimAction;
    [SerializeField] InputActionReference _vpAction;
    
    [SerializeField] Animator _zoomAnimator;

    // =================================================
    // 스코프 관련 설정
    // =================================================
    [Header("Scope")]
    [SerializeField] Camera _scopeCamera;
    [SerializeField] GameObject _scopeUI;

    // =================================================
    // 시야각 설정
    // =================================================
    [Header("Zoom")]
    [SerializeField] float _curFOV = 15f;
    [SerializeField] float _maxFOV = 15f;
    [SerializeField] float _minFOV = 4f;
    [SerializeField] float _scopeSensitivity = 0.25f;

    // =================================================
    // 상태값
    // =================================================
    bool _isAiming = false;

    public float CurrentFOV => _curFOV;
    public float MaxFOV => _maxFOV;
    public bool IsAiming => _isAiming;

    // =================================================
    // Input System 조준 입력 발생시 실행할 함수
    // =================================================
    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (!_isAiming)
        {
            _zoomAnimator.SetBool("isAim", true);
        }
        else
        {
            EndScope();
        }
    }

    // =================================================
    // StartScope
    // =================================================
    public void StartScope()
    {
        _isAiming = true;

        _scopeCamera.enabled = true;
        _scopeUI.SetActive(true);
    }

    // =================================================
    // EndScope
    // =================================================
    public void EndScope()
    {
        _isAiming = false;

        _scopeCamera.enabled = false;
        _scopeUI.SetActive(false);
        _zoomAnimator.SetBool("isAim", false);

        _scopeCamera.fieldOfView = _curFOV;
    }

    // =================================================
    // 가변배율
    // =================================================
    public void VariablePower()
    {
        Vector2 scroll = _vpAction.action.ReadValue<Vector2>();

        if(scroll.y > 0f)
        {
            _curFOV -= _scopeSensitivity;
        }
        else if (scroll.y < 0f)
        {
            _curFOV += _scopeSensitivity;
        }

        _curFOV = Mathf.Clamp( _curFOV, _minFOV, _maxFOV);

        _scopeCamera.fieldOfView = _curFOV;
    }

    // =================================================
    // Awake
    // =================================================
    void Awake()
    {
        _curFOV = _maxFOV;

        _zoomAnimator = GetComponentInChildren<Animator>();
    }

    // =================================================
    // OnEnable
    // =================================================
    void OnEnable()
    {
        _aimAction.action.started += OnAim;

        _aimAction.action.Enable();
        _vpAction.action.Enable();
    }

    // =================================================
    // OnDisable
    // =================================================
    void OnDisable()
    {
        _aimAction.action.started -= OnAim;

        _aimAction.action.Disable();
        _vpAction.action.Disable();
    }

    // =================================================
    // Update
    // =================================================
    void Update()
    {
        if (!_isAiming)
            return;

        VariablePower();
    }
}
