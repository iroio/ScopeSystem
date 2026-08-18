using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    BulletGenerator _bulletGenerator;

    [SerializeField] BulletData _data;

    Vector3 _velocity;

    float _drag = 0.0000016f;

    // =========================================================
    // 초기화
    // =========================================================
    public void InitBullet(BulletGenerator bullet, Vector3 direction)
    {
        _bulletGenerator = bullet;
        _velocity = direction.normalized * _data.muzzleVelocity;
    }

    public void CheckBulletState()
    {
        if (_bulletGenerator == null) return;

        // 중력 계산
        _velocity += Physics.gravity * Time.deltaTime;

        // 항력
        float speed = _velocity.magnitude;
        Vector3 drag = _velocity.normalized * speed * speed * _drag;

        // 현재 프레임 이동량
        Vector3 movement = _velocity * Time.deltaTime;

        Vector3 start = transform.position;
        Vector3 direction = movement.normalized;
        float distance = movement.magnitude;

        if (Physics.Raycast(start, direction, out RaycastHit hit, distance))
        {
            transform.position = hit.point;

            // 탄환 제거 로직

            return;
        }

        transform.position += movement;

        if (_velocity.sqrMagnitude > 0.001f)
        {
            transform.forward = _velocity.normalized;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckBulletState();
    }
}
