using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField]private Image _image;

    public void SetKitchenObjectSOImage(KitchenObjectsSO kitchenObjectSO)
    {
        _image.sprite = kitchenObjectSO.sprite;
    }
}
