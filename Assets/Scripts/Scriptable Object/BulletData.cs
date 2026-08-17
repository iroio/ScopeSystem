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

    // ÃÑ±¸ ¼Óµµ
    public float muzzleVelocity;
    // ÅºÀÚ¹«°Ô
    public float projectileWeight;
}
