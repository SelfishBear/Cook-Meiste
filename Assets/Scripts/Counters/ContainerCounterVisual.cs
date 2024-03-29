using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
   private const string OPEN_CLOSE = "OpenClose";
   [SerializeField] private ContainerCounter _containerCounter;
   private Animator animator;

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      _containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
   }

   private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
   {
      animator.SetTrigger(OPEN_CLOSE);
   }
}
