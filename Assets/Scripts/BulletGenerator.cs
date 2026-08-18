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
        Vector3 direction = _muzzle.forward;

        _currentBullet = Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
        _currentBullet.GetComponent<BulletMovement>().InitBullet(this, direction);
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
        }
    }
}
