using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO _kitchenObjectsSO;
    }
        
    [SerializeField] private List<KitchenObjectsSO> _validKitchenObjectsSOList;
    
    private List<KitchenObjectsSO> _kitchenObjectsSOList;

    private void Awake()
    {
        _kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        if (!_validKitchenObjectsSOList.Contains(kitchenObjectsSO))
            return false;
        if (_kitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            return false;
        }
        else
        {
            _kitchenObjectsSOList.Add(kitchenObjectsSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                _kitchenObjectsSO = kitchenObjectsSO
            });
            
            return true;
        }
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList()
    {
        return _kitchenObjectsSOList;
    }
}