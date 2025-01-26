using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectList;

    private void Awake()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach ( KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList )
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e._kitchenObjectsSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}