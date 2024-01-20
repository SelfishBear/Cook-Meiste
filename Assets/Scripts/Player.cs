using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    
    
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _rotSpeed = 0f;
    [SerializeField] private LayerMask counterLayerMask;
    
    
    public GameInput _gameInput;
    private bool _isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObjects _kitchenObjects;
    [SerializeField] private Transform kitchenObjectHoldPoint;



    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }

        Instance = this;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        float interactDistance = 2f; 
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                   SetSelectedCounter(baseCounter);
                }
            }
            else
            {
               SetSelectedCounter(null); 
               
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        float _moveDistance = _speed * Time.deltaTime;
        float _playerRadius = 0.5f;
        float _playerHeight = 2f;
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        bool _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDir, _moveDistance);
        if (!_canMove)
        {
            //Нельзя идти по modeDir;
            //Попытка ходьбы по X;
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirX, _moveDistance);
            if (_canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirZ, _moveDistance);
                if (_canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //нету движения по осям
                }
            }
        }
        if (_canMove)
        {
         transform.position += moveDir * _moveDistance;
        }
        
        _isWalking = moveDir != Vector3.zero; 
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotSpeed);
    }
    public bool IsWalking()
    {
        return _isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }


    public Transform GetKitchenFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObjects(KitchenObjects _kitchenObjects)
    {
        this._kitchenObjects = _kitchenObjects;
    }

    public KitchenObjects GetKitchenObjects()
    {
        return _kitchenObjects;
    }

    public void ClearKitchenObjects()
    { 
        _kitchenObjects = null;
    }

    public bool HasKitchenObjects()
    {
        return _kitchenObjects != null;
    }
}

