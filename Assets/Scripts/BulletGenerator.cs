using UnityEngine;
using UnityEngine.InputSystem;

public class BulletGenerator : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] InputActionReference _fire;
    [SerializeField] Transform _bullet;
    [SerializeField] Transform _muzzle;

    [Header("Data")]
    [SerializeField] BulletData _data;

    Transform _currentBullet;

    public void SpawnBullet()
    {
        Vector3 direction = _muzzle.forward;

        _currentBullet = Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
        _currentBullet.GetComponent<BulletMovement>().InitBullet(this, direction);
    }


    //========================================================= 
    // 탄환 피격 체크 
    // ========================================================= 
    public void BulletHitCheck()
    {
        Debug.Log("BulletHitCheck 실행");

        Vector3 origin = _muzzle.position;
        Vector3 direction = _muzzle.forward;
        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            float distance = hit.distance;
            float flightTime = distance / _data.muzzleVelocity;
            Debug.Log($"거리 : {distance:F1}m");
            Debug.Log($"비행 시간 : {flightTime:F3}s");
        }
        else
        {
            Debug.Log("Raycast가 아무것도 맞추지 못함");
        }
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
            SpawnBullet();
            BulletHitCheck();
        }
    }
}
