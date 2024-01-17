using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenFollowTransform();


    public void SetKitchenObjects(KitchenObjects _kitchenObjects);


    public KitchenObjects GetKitchenObjects();


    public void ClearKitchenObjects();


    public bool HasKitchenObjects();

}