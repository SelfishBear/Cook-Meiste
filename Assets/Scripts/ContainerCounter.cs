using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{


    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectsSO _kitchenObjectsSo;
   
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObjects())
        {
            //Player bot carrying anything
            KitchenObjects.SpawnKitchenObjects(_kitchenObjectsSo, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
   
}
