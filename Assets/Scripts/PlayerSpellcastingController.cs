using System;
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

    public float lrWidth = 0.01f;

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
        if (!performed)
        {
            hitObject = null;
            return;
        }

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
        ChannelBetweenObjects(handTransform.position, hitObject.transform.position);
        // positions = new[] { handTransform.position, hitObject.transform.position };
        // positions = CalculateSinePositions();
        // lineRenderer.positionCount = positions.Length;
        // lineRenderer.SetPositions(positions);
    }


    public float amplitude = 0.01f;
    public float frequency = 10;
    public float movementSpeed = 10;
    public float tau = 2 * Mathf.PI;

    private void ChannelBetweenObjects(Vector3 startingPosition, Vector3 endingPosition)
    {
        var distance = (int)Vector3.Distance(handTransform.position, hitObject.transform.position) * 100;
        lineRenderer.startWidth = lrWidth;
        lineRenderer.endWidth = lrWidth * 10;
        lineRenderer.positionCount = distance;
        for (var point = 0; point < distance; point++)
        {
            var progress = (float)point / (distance - 1);
            var x = Mathf.Lerp(startingPosition.x, endingPosition.x, progress);
            var y = amplitude * Mathf.Sin(tau * frequency * x + Time.timeSinceLevelLoad * movementSpeed);
            var yy = Mathf.Lerp(startingPosition.y, endingPosition.y, progress) + y;
            var z = Mathf.Lerp(startingPosition.z, endingPosition.z, progress);
            lineRenderer.SetPosition(point, new Vector3(x, yy, z));
        }
    }
}