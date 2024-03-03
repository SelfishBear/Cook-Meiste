using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{


    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObjects())
        {
            //No kitchen object
            if (player.HasKitchenObjects())
            {
                //Player carrying something
                if (HasRecipeWithInput(player.GetKitchenObjects().GetKitchenObjectsSo()))
                {
                    //Player carrying something that can be CUT
                    player.GetKitchenObjects().SetKitchenObjectParent(this);
                }
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
        if (HasKitchenObjects() && HasRecipeWithInput(GetKitchenObjects().GetKitchenObjectsSo()))
        {
            //There is a KO here and it can be cut
            KitchenObjectsSO outputKitchenObjectsSO = GetOutputForInput(GetKitchenObjects().GetKitchenObjectsSo());
            GetKitchenObjects().DestroySelf();
            KitchenObjects.SpawnKitchenObjects(outputKitchenObjectsSO, this);
        }
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in _cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectsSo)
            {
                return cuttingRecipeSo.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in _cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectsSo)
            {
                return true;
            }
        }

        return false;
    }
}
