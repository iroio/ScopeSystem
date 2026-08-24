using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public void AnimStartScope()
    {
        ScopeController scopeController = FindFirstObjectByType<ScopeController>();

        if (scopeController == null) return;

        scopeController.StartScope();
    }
}
