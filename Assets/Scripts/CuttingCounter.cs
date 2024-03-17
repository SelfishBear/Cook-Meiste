using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
        
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
    private int _cuttingProgress;

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
                    _cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSo());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
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
            _cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSo());
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectsSO outputKitchenObjectsSO = GetOutputForInput(GetKitchenObjects().GetKitchenObjectsSo());
                GetKitchenObjects().DestroySelf();
                KitchenObjects.SpawnKitchenObjects(outputKitchenObjectsSO, this);
            }
        }
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSo);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSo);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in _cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectsSo)
            {
                return cuttingRecipeSo;
            }
        }

        return null;
    }
}