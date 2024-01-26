using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{


    [SerializeField] private KitchenObjectsSO cutKitchenObjectsSo;
    public override void Interact(Player player)
    {
        if (!HasKitchenObjects())
        {
            //No kitchen object
            if (player.HasKitchenObjects())
            {
                //Player carrying something
                player.GetKitchenObjects().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //Here is KO
            if (player.HasKitchenObjects())
            {
                //Player carrying something
            }
            else
            {
                //Player not carrying anything
                GetKitchenObjects().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObjects())
        {
            //There is a KO here
            GetKitchenObjects().DestroySelf();
            KitchenObjects.SpawnKitchenObjects(cutKitchenObjectsSo, this);
        }
    }
}
