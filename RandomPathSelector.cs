using UnityEngine;
using UnityEngine.AI;

// Manages random path selection for an agent.
public class RandomPathSelector : MonoBehaviour
{
    [SerializeField] // Use SerializeField to keep agent private but settable in inspector
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    [SerializeField]
    private Transform[] possibleDestinations; // Holds possible destinations for the agent

    // Initialize the component.
    private void Start()
    {
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is not assigned in the inspector.", this);
            return;
        }
        
        ChooseRandomDestination();
    }

    // Chooses a random destination from the possible destinations and sets the agent's destination.
    private void ChooseRandomDestination()
    {
        if (possibleDestinations == null || possibleDestinations.Length == 0)
        {
            Debug.LogError("No destinations are set for the agent.", this);
            return;
        }

        // Pick a random index from the possible destinations
        int destinationIndex = Random.Range(0, possibleDestinations.Length);

        // Set the agent's destination
        agent.SetDestination(possibleDestinations[destinationIndex].position);
    }
}
