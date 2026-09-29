using UnityEngine;

public class floatingHover : MonoBehaviour
{
    [Header("Float Settings")]
    [Tooltip("How fast the orb moves up and down")]
    public float floatSpeed = 2f;      
    
    [Tooltip("How far up and down the orb moves from its starting position")]
    public float floatAmplitude = 0.2f;  

    private Vector3 startPosition;
    private float uniqueOffset;

    void Start()
    {
        // Remember the original position of the orb
        startPosition = transform.position;

        // Generate a unique offset based on where the orb is placed in the world.
        // This stops the orbs from moving in perfect synchronization.
        uniqueOffset = transform.position.x + transform.position.z;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave shifted by our unique offset
        Vector3 tempPos = startPosition;
        tempPos.y += Mathf.Sin((Time.time * floatSpeed) + uniqueOffset) * floatAmplitude;
        
        // Apply the new position to the orb
        transform.position = tempPos;
    }

    /// <summary>
    /// Call this if the object is physically moved to a new spot, 
    /// so it hovers around its new location instead of snapping back.
    /// </summary>
    public void UpdateStartPosition(Vector3 newPos)
    {
        startPosition = newPos;
        uniqueOffset = newPos.x + newPos.z; // Recalculate offset for the new spot
    }
}
