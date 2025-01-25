using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;
    private List<GameObject> _plateVisualList;

    private void Awake()
    {
        _plateVisualList = new List<GameObject>();
        _platesCounter.OnPlateSpawned += PlateCounter_OnOnPlateSpawned;
        _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = _plateVisualList[_plateVisualList.Count - 1];
        _plateVisualList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnOnPlateSpawned(object sender, EventArgs e)
    {
        float plateOffsetY = .1f;
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualList.Count, 0);
        _plateVisualList.Add(plateVisualTransform.gameObject);
    }
}