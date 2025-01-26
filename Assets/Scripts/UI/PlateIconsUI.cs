using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
   [SerializeField] private PlateKitchenObject _plateKitchenObject;
   [SerializeField] private Transform _iconUI;
   private void Awake()
   {
      _iconUI.gameObject.SetActive(false);
      _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded; 
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
   {
      UpdateVisual();
   }

   private void UpdateVisual()
   {
      foreach (Transform child in transform)
      {
         if(child == _iconUI) continue;
         Destroy(child.gameObject);
      }
      foreach (KitchenObjectsSO kitchenObjectsSo in _plateKitchenObject.GetKitchenObjectsSOList())
      {
         Transform iconTransform = Instantiate(_iconUI, transform);
         iconTransform.gameObject.SetActive(true);
         iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSOImage(kitchenObjectsSo);
      }
   }
}


