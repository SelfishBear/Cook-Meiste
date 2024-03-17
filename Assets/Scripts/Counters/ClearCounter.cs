using UnityEngine;

public class ClearCounter : BaseCounter
{


   [SerializeField] private KitchenObjectsSO _kitchenObjectsSo;
   
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

   

}
