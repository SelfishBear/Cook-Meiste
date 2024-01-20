using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{


    [SerializeField] private GameObject[] _visualGameObjectArray;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(false);
        }
    }
}
