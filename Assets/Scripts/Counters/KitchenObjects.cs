using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO _kitchenObjectsSo;
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectsSo()
    {
        return _kitchenObjectsSo;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObjects();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObjects())
        {
            Debug.LogError(" IKitchenObjectParent has object ");
        }
        kitchenObjectParent.SetKitchenObjects(this);
        transform.parent = kitchenObjectParent.GetKitchenFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObjects();
        Destroy(gameObject);
    }


    public static KitchenObjects SpawnKitchenObjects(KitchenObjectsSO kitchenObjectsSo, IKitchenObjectParent kitchenObjectParent)
    {
        Transform _kitchenObjectsTransform = Instantiate(kitchenObjectsSo.prefab);
        KitchenObjects kitchenObject = _kitchenObjectsTransform.GetComponent<KitchenObjects>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
