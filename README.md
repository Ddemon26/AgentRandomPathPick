# RandomPathSelector

The `RandomPathSelector` script is a Unity component that allows you to randomly select a path for an agent to follow using Unity's NavMesh system. This is useful for creating unpredictable movement patterns for NPCs or other game entities.

## Usage

To use this script, attach it to a GameObject that has a NavMeshAgent component. Then, assign the waypoints where the agent can move in the `Points` array in the Inspector.

The agent will randomly select one of these waypoints and move towards it. Once it reaches the waypoint, it will select a new waypoint and continue moving.

## Configuration

- **Points:** An array of Transform objects that represent the waypoints the agent can move to.
- **Agent:** The NavMeshAgent component attached to the GameObject. This is automatically assigned when the script is attached.

## Example

```csharp
public class ExampleUsage : MonoBehaviour
{
    private RandomPathSelector pathSelector;

    void Start()
    {
        pathSelector = GetComponent<RandomPathSelector>();
        pathSelector.Points = new Transform[] { /* Assign your waypoints here */ };
    }
}
```
