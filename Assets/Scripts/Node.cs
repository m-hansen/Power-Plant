using UnityEngine;

// The node script is expected to be attached to the root of it's corresponding GameObject.
// It will relay user input along with data for the backend graph structure.
public class Node : MonoBehaviour
{
    public int Depth { get; private set; }
}
