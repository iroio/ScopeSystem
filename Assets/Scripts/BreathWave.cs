using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreathWave : MonoBehaviour
{
    // =================================================
    // Reference
    // =================================================
    [Header("Reference")]
    [SerializeField] Transform _scopePivot;
    [SerializeField] ScopeController _scopeController;
    [SerializeField] InputActionReference _holdBreath;

    Coroutine _holdBreathCoroutine;

    // =================================================
    // Èçµé¸² Á¶Àý ¼öÄ¡
    // =================================================
    [Header("Breeath Wave Options")]
    [SerializeField] float _breathSpeed = 2f;
    [SerializeField] float _breathPitch;
    [SerializeField] float _breathYaw;

    [Space]
    [SerializeField] float _holdBreathTime = 5f;
    [SerializeField] float _breathMultiplier = 1f;
    [SerializeField] float _breathControlTime = 0.5f;

    float _breathTime = 0f;

    // =================================================
    // ¼ûÂü±â °è»ê
    // =================================================
    IEnumerator CoBreathContol()
    {
        float start = _breathMultiplier;
        float time = 0f;

        // Èçµé¸² °¨¼Ò
        while (time < _breathControlTime)
        {
            time += Time.deltaTime;

            float t = time / _breathControlTime;
            t = Mathf.SmoothStep(0f, 1f, t);

            _breathMultiplier = Mathf.Lerp(start, 0.2f, t);

            yield return null;
        }

        _breathMultiplier = 0.15f;

        time = 0f;

        // ¼ûÂü±â ½Ã°£
        while (time < _holdBreathTime)
        {
            if (!_holdBreath.action.IsPressed())
                break;

            time += Time.deltaTime;

            yield return null;
        }

        start = _breathMultiplier;
        time = 0f;

        // Èçµé¸² º¹±Í
        while (time < _breathControlTime)
        {
            time += Time.deltaTime;

            float t = time / _breathControlTime;
            t = Mathf.SmoothStep(0f, 1f, t);

            _breathMultiplier = Mathf.Lerp(start, 1f, t);

            yield return null;
        }

        _breathMultiplier = 1f;
        _holdBreathCoroutine = null;
    }

    // =================================================
    // È£Èí Èçµé¸²
    // =================================================
    public void DefaultBreathWave()
    {
        if (!_scopeController.IsAiming)
        {
            _breathTime = 0;
            return;
        }

        _breathTime += _breathSpeed * Time.deltaTime;

        float pitch = Mathf.Sin(_breathTime) * _breathPitch * _breathMultiplier;
        float yaw = Mathf.Sin(_breathTime * 0.7f) * _breathYaw * _breathMultiplier;

        _scopePivot.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    // =================================================
    // Update
    // =================================================
    void Update()
    {
        DefaultBreathWave();

        if(_holdBreath.action.WasPressedThisFrame())
        {
            if(_holdBreathCoroutine != null)
            {
                StopCoroutine( _holdBreathCoroutine );
            }

            _holdBreathCoroutine = StartCoroutine(CoBreathContol());
        }
    }
}
