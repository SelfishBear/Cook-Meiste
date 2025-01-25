using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _counterTopPoint;
    private KitchenObjects _kitchenObjects;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObjects(KitchenObjects _kitchenObjects)
    {
        this._kitchenObjects = _kitchenObjects;
    }

    public KitchenObjects GetKitchenObjects()
    {
        return _kitchenObjects;
    }

    public void ClearKitchenObjects()
    {
        _kitchenObjects = null;
    }

    public bool HasKitchenObjects()
    {
        return _kitchenObjects != null;
    }
}