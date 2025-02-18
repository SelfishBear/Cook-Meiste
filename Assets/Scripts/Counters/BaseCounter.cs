using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;
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
       
        if (_kitchenObjects != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
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