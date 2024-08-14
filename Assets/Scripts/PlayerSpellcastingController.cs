using System.Collections.Generic;
using InputEngine;
using Spellcasting.SpellcastingStates;
using StateMachineEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpellcastingController : BaseController
{
    private Animator animator;
    private InputReader inputReader;
    private Camera camera;
    private LineRenderer lineRenderer;


    public override void SetupStateMachine()
    {
        StateMachine = new StateMachine();

        var harvestingState = new HarvestingState(animator, () => { });
        var baseState = new NoState(animator, () => { });

        // stateMachine.AddTransition(baseState, harvestingState, new FunctionPredicate(() => jumpTimer.IsRunning));
        StateMachine.AddAnyTransition(baseState, new FunctionPredicate(() => true));

        StateMachine.SetState(baseState);
    }

    public override void SetupReferences()
    {
        animator = GetComponent<Animator>();
        inputReader = ScriptableObject.CreateInstance<InputReader>();
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        handTransform = FindChild(transform, "mixamorig:RightHand");
        Debug.Log(handTransform);
    }

    private Transform FindChild(Transform source, string childName)
    {
        var result = source;
        foreach (Transform child in source)
        {
            if (result.name == childName) break;
            result = FindChild(child, childName);
        }

        return result;
    }

    public override void SetupTimers()
    {
        Timers = new List<Timer.Timer>();
    }

    private void Start() => inputReader.EnablePlayerActions();

    private void OnEnable()
    {
        inputReader.HarvestElement += OnHarvestElement;
    }

    private void OnDisable()
    {
        inputReader.HarvestElement -= OnHarvestElement;
    }


    private void OnHarvestElement(bool performed)
    {
        if (!performed) return;
        var mousePosition = Mouse.current.position;
        var ray = camera.ScreenPointToRay(new Vector3(mousePosition.x.value, mousePosition.y.value));
        if (!Physics.Raycast(ray, out var hitInfo)) return;
        // Debug.Log(hitInfo.transform.gameObject.name);
        hitObject = hitInfo.transform.gameObject;
        // var rotation = Quaternion.LookRotation(hitObject.transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    private Vector3[] positions;
    private GameObject hitObject;
    private Transform handTransform;

    private void Update()
    {
        lineRenderer.positionCount = 0;
        if (!hitObject) return;
        positions = new[] { handTransform.position, hitObject.transform.position };
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(positions);
    }
}