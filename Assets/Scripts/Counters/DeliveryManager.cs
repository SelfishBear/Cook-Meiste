using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompeleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;


    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4;
    private int _waitingRecipesMax = 4;
    private int _successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0.0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;
            if (_waitingRecipeSOList.Count < _waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO =
                    _recipeListSO.RecipeSOList[Random.Range(0, _recipeListSO.RecipeSOList.Count)];
                _waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

            if (waitingRecipeSO.KitchenObjects.Count == plateKitchenObject.GetKitchenObjectsSOList().Count)
            {
                bool plateContentMathesRecipe = true;

                foreach (KitchenObjectsSO recipeKitchenObjectsSO in waitingRecipeSO.KitchenObjects)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {
                        if (plateKitchenObjectsSO == recipeKitchenObjectsSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentMathesRecipe = false;
                    }
                }

                if (plateContentMathesRecipe)
                {
                    _waitingRecipeSOList.RemoveAt(i);
                    _successfulRecipesAmount++;

                    OnRecipeCompeleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipes()
    {
        return _waitingRecipeSOList;
    }

    public int GetSuccessfulRecipes()
    {
        return _successfulRecipesAmount;
    }
}