using UnityEngine;

public enum BulletType
{
    m993
}

[CreateAssetMenu(menuName = "Bullet/Row Data")]
public class BulletData : ScriptableObject
{
    public BulletType bulletType;

    public GameObject prefab;

    // 총구 속도
    public float muzzleVelocity;
    // 탄자무게
    public float projectileWeight;
    // 최대 사거리
    public float maxRange;
}
