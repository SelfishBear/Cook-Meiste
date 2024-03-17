using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; 

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSoArray;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO _fryingRecipeSo;
    private BurningRecipeSO _burningRecipeSo;
    private State _state;


    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObjects())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / _fryingRecipeSo.fryingTimerMax
                    });
                    if (fryingTimer > _fryingRecipeSo.fryingTimerMax)
                    {
                        //Fried

                        GetKitchenObjects().DestroySelf();

                        KitchenObjects.SpawnKitchenObjects(_fryingRecipeSo.output, this);
                        _state = State.Fried;
                        burningTimer = 0f;
                        _burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSo());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {

                           state = _state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / _burningRecipeSo.burningTimerMax
                    });
                    if (burningTimer > _burningRecipeSo.burningTimerMax)
                    {
                        //Fried

                        GetKitchenObjects().DestroySelf();

                        KitchenObjects.SpawnKitchenObjects(_burningRecipeSo.output, this);
                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {

                            state = _state
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObjects())
        {
            //No kitchen object
            if (player.HasKitchenObjects())
            {
                //Player carrying something
                if (HasRecipeWithInput(player.GetKitchenObjects().GetKitchenObjectsSo()))
                {
                    //Player carrying something that can be Fried
                    player.GetKitchenObjects().SetKitchenObjectParent(this);

                    _fryingRecipeSo = GetFryingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSo());
                    _state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {

                        state = _state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / _fryingRecipeSo.fryingTimerMax
                    });
                }
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
                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {

                    state = _state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectsSo);
        return fryingRecipeSo != null;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectsSo);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        foreach (FryingRecipeSO fryingRecipeSo in _fryingRecipeSoArray)
        {
            if (fryingRecipeSo.input == inputKitchenObjectsSo)
            {
                return fryingRecipeSo;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSo)
    {
        foreach (BurningRecipeSO burningRecipeSo in _burningRecipeSoArray)
        {
            if (burningRecipeSo.input == inputKitchenObjectsSo)
            {
                return burningRecipeSo;
            }
        }
        return null;
    }
}