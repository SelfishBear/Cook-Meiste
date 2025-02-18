using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DeliveryManagerSingleUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI recipeNameText;
   [SerializeField] private Transform _iconContainer;
   [SerializeField] private Transform _iconTemplate;


   private void Awake()
   {
      _iconTemplate.gameObject.SetActive(false);
   }

   public void SetRecipeSO(RecipeSO recipeSO)
   {
      recipeNameText.text = recipeSO.RecipeName;

      foreach (Transform child in _iconContainer)
      {
         if (child == _iconTemplate) continue;
         Destroy(child.gameObject);
      }

      foreach (KitchenObjectsSO kitchenObjectsSo in recipeSO.KitchenObjects)
      {
         Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
         iconTransform.gameObject.SetActive(true);
         iconTransform.GetComponent<Image>().sprite = kitchenObjectsSo.sprite;
      }
   }
   
}
