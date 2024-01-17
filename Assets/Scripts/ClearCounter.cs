using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{


   [SerializeField] private KitchenObjectsSO _kitchenObjectsSo;
   [SerializeField] private Transform _counterTopPoint;
    private KitchenObjects _kitchenObjects;

   
   
    public void Interact(Player player)
    {
        if (_kitchenObjects == null)
        {
            Transform _kitchenObjectsTransform = Instantiate(_kitchenObjectsSo.prefab, _counterTopPoint);
            _kitchenObjectsTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(this);

        }
        else
        {
            //Give to the player
            _kitchenObjects.SetKitchenObjectParent(player);
        }
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
