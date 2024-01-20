using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{


    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectsSO _kitchenObjectsSo;
   
    
    public override void Interact(Player player)
    {
        Transform _kitchenObjectsTransform = Instantiate(_kitchenObjectsSo.prefab);
        _kitchenObjectsTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
   
}
