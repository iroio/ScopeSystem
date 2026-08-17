using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    BulletGenerator _bulletGenerator;

    [SerializeField] BulletData _data;

    Transform _muzzle;

    // =========================================================
    // √ ±‚»≠
    // =========================================================
    public void InitBullet(BulletGenerator bullet, Transform muzzle)
    {
        _bulletGenerator = bullet;
        _muzzle = muzzle;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_bulletGenerator == null) return;

        transform.position += _muzzle.forward * _data.muzzleVelocity * Time.deltaTime;
    }
}
