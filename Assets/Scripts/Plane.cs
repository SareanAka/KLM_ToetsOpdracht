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

    private void Awake()
    {
        pointLight = GetComponentInChildren<Light>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Start that sets one initial destination
    /// </summary>
    void Start()
    {
        SetWanderingDestination();
    }

    /// <summary>
    /// Sets a sets the plane to a hangar
    /// </summary>
    /// <param name="hanger">Hanger with the corosponding number </param>
    public void SetHanger(Hanger hanger)
    {
        hangerPos = hanger.gameObject.transform.position;
    }

    /// <summary>
    /// Select a random destination point
    /// </summary>
    /// <param name="origin">Transform of the object</param>
    /// <param name="distance">Max distance the random pos can be</param>
    /// <param name="layermask">In which layermask the randomizer will search</param>
    /// <returns></returns>
    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMesh.SamplePosition(randomDirection, out var navHit, distance, layermask);

        return navHit.position;
    }

    /// <summary>
    /// Update checks every frame if the object has reached it's destination yet
    /// </summary>
    void Update()
    {
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

    /// <summary>
    /// Sets a new destination
    /// </summary>
    private void SetWanderingDestination()
    {
        destination = RandomNavSphere(transform.position, wanderDistance, -1);
        navMeshAgent.SetDestination(destination);
    }

    /// <summary>
    /// Sets the hangar as destination
    /// </summary>
    private void NavigateToHanger()
    {
        destination = hangerPos;
        navMeshAgent.SetDestination(hangerPos);
    }

    /// <summary>
    /// Toggles if the planes are parking/parked or not
    /// </summary>
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

    /// <summary>
    /// Toggles the lights
    /// </summary>
    public void ToggleLight()
    {
        pointLight.enabled = !pointLight.enabled;
    }
}
