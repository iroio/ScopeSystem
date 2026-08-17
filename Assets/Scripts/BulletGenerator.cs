using UnityEngine;
using UnityEngine.InputSystem;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] InputActionReference _fire;
    [SerializeField] Transform _bullet;
    [SerializeField] Transform _muzzle;

    [SerializeField] BulletData _data;

    Transform _currentBullet;

    public void SpawnBullet()
    {
        _currentBullet = Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
        _currentBullet.GetComponent<BulletMovement>().InitBullet(this, _muzzle);
    }

    // =========================================================
    // 탄환 피격 체크
    // =========================================================
    public void BulletHitCheck()
    {
        Vector3 origin = _muzzle.position;
        Vector3 direction = _muzzle.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            float distance = hit.distance;
            float flightTime = distance / _data.muzzleVelocity;

            Debug.Log($"거리 : {distance:F1}m");
            Debug.Log($"비행 시간 : {flightTime:F3}s");

            Hit(hit);
        }
    }

    // =========================================================
    // 피격 로그
    // =========================================================
    void Hit(RaycastHit hit)
    {
        Debug.Log($"명중 : {hit.collider.name}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_fire.action.WasPressedThisFrame())
        {
            BulletHitCheck();
            SpawnBullet();
        }
    }
}
