using UnityEngine;
using UnityEngine.InputSystem;

public class ScopeController : MonoBehaviour
{
    // =================================================
    // Reference
    // =================================================
    [Header("Reference")]
    [SerializeField] InputActionReference _zoomAction;

    // =================================================
    // 스코프 관련 설정
    // =================================================
    [Header("Scope")]
    [SerializeField] Camera _scopeCamera;
    [SerializeField] GameObject _scopeUI;
    [SerializeField] GameObject _scopeSide;

    // =================================================
    // 시야각 설정
    // =================================================
    [Header("Zoom")]
    [SerializeField] float _curFOV = 15f;
    [SerializeField] float _maxFOV = 15f;
    [SerializeField] float _minFOV = 4f;
    [SerializeField] float _scopeSensitivity = 0.5f;

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
        // 토글 방식
        if (!ctx.started)
            return;

        if (_isAiming)
        {
            EndScope();
        }
        else
        {
            StartScope();
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
        _scopeSide.SetActive(true);
    }

    // =================================================
    // EndScope
    // =================================================
    public void EndScope()
    {
        _isAiming = false;

        _scopeCamera.enabled = false;
        _scopeUI.SetActive(false);
        _scopeSide.SetActive(false);

        _scopeCamera.fieldOfView = _curFOV;
    }

    // =================================================
    // 가변배율
    // =================================================
    public void Zoom()
    {
        Vector2 scroll = _zoomAction.action.ReadValue<Vector2>();

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

    void Awake()
    {
        _curFOV = _maxFOV;
    }

    void Update()
    {
        if (!_isAiming)
            return;

        Zoom();
    }
}
