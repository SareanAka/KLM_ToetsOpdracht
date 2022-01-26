using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plane : BaseObj
{
    [SerializeField] private float wanderDistance = 8f;
    [SerializeField] private float minDistanceToDest = 0.5f;
    public bool IsParked => isParked;

    private NavMeshAgent navMeshAgent;
    private Light pointLight;
    private Vector3 destination;
    private Vector3 hangerPos;
    private bool parking;
    private bool isParked;
    private float distance;

    private void Awake()
    {
        pointLight = GetComponentInChildren<Light>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWanderingDestination();
    }

    public void SetHanger(Hanger hanger)
    {
        hangerPos = hanger.gameObject.transform.position;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMesh.SamplePosition(randomDirection, out var navHit, distance, layermask);

        return navHit.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, destination);
        if (Vector3.Distance(transform.position, destination) <= minDistanceToDest)
        {
            if (!parking)
            {
                SetWanderingDestination();
            }
            else
            {
                isParked = true;
            }
        }
    }

    private void SetWanderingDestination()
    {
        destination = RandomNavSphere(transform.position, wanderDistance, -1);
        navMeshAgent.SetDestination(destination);
    }

    private void NavigateToHanger()
    {
        destination = hangerPos;
        navMeshAgent.SetDestination(hangerPos);
    }

    public void TogglePark()
    {
        parking = !parking;
        if (!parking)
        {
            SetWanderingDestination();
            isParked = false;
        }
        else
        {
            NavigateToHanger();
        }
    }

    public void ToggleLight()
    {
        pointLight.enabled = !pointLight.enabled;
    }
}
