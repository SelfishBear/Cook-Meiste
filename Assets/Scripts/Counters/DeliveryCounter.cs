using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
   public static DeliveryCounter Instance {get; private set;}

   private void Awake()
   {
       Instance = this;
   }

   public override void Interact(Player player)
   {
      if (player.HasKitchenObjects())
      {
         if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
         {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.GetKitchenObjects().DestroySelf();
         }
         
      }
   }
   
}
