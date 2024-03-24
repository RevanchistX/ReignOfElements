   // public float ThinkInterval;
   //  public float SearchRadius;
   //  public LayerMask SearchLayerMask;
   //  public float JumpStrength;
   //  public float HopDistance;
   //  public int CurrentFood;
   //  public bool IsHungry;
   //  public float BreakDuration;
   //  public int HungerLimit;
   //  public int Hunger
   //  {
   //      get
   //      {
   //          return _hunger;
   //      }
   //      set
   //      {
   //          _hunger = value;
   //          if (_hunger > HungerLimit)
   //          {
   //              _hunger = 0;
   //              TakeABreak();
   //          }
   //      }
   //  }
   //
   //  [SerializeField] private Rigidbody rb;
   //
   //  private GameObject foodTarget;
   //  private int _hunger;
   //
   //  private void Start()
   //  {
   //      Invoke("Think", ThinkInterval);
   //  }
   //
   //  private void Think()
   //  {
   //      //Debug.Log($"Creature {name} is thinking");
   //      SearchForFood();
   //
   //      ChaseFood();
   //
   //      Invoke("Think", ThinkInterval);
   //  }
   //
   //  private void SearchForFood()
   //  {
   //      Collider[] detectedObjects = Physics.OverlapSphere(transform.position, SearchRadius, SearchLayerMask);
   //      for (int i = 0; i < detectedObjects.Length; i++)
   //      {
   //          if (detectedObjects[i].GetComponent<Food>())
   //          {
   //              foodTarget = detectedObjects[i].gameObject;
   //              return;
   //          }
   //      }
   //  }
   //
   //  private void ChaseFood()
   //  {
   //      if (foodTarget == null || !IsHungry)
   //      {
   //          return;
   //      }
   //
   //      Vector3 dir = (foodTarget.transform.position - transform.position).normalized;
   //
   //      rb.AddForce(dir * JumpStrength + Vector3.up * HopDistance);    
   //  }
   //
   //  private void TakeABreak()
   //  {
   //      rb.velocity = Vector3.zero;
   //      IsHungry = false;
   //      Invoke(nameof(StopBreak), BreakDuration);
   //  }
   //
   //  private void StopBreak()
   //  {
   //      IsHungry = true;
   //  }
   //
   //  private void OnTriggerEnter(Collider other)
   //  {
   //      Food food = other.GetComponent<Food>();
   //      if (food != null)
   //      {
   //          Hunger++;
   //          CurrentFood += food.FoodValue;
   //          food.gameObject.SetActive(false);
   //      }
   //  }