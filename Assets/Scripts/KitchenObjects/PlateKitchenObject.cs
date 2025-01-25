using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
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
            return true;
        }
    }
}