using UnityEngine;

public class wandInteractions : MonoBehaviour
{
    [Header("Green Orb Settings")]
    public Material newGreenMaterial; // Assign the new material in the Inspector

    [Header("Pink Orb Settings")]
    [Tooltip("How much larger the pink orb gets (e.g., 1.5 means 150% of original size)")]
    public float growthFactor = 1.5f; 

    // Tracks if the pink orb is currently in its enlarged state
    private bool isPinkOrbBig = false;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Orange Orb: Disappear
        if (other.CompareTag("orangeOrb"))
        {
            Destroy(other.gameObject);
        }
        
        // 2. Green Orb: Switch Material
        else if (other.CompareTag("greenOrb"))
        {
            Renderer orbRenderer = other.GetComponent<Renderer>();
            if (orbRenderer != null && newGreenMaterial != null)
            {
                orbRenderer.material = newGreenMaterial;
            }
        }
        
        // 3. Pink Orb: Alternating Grow and Shrink
        else if (other.CompareTag("pinkOrb"))
        {
            if (!isPinkOrbBig)
            {
                // Grow the orb
                other.transform.localScale *= growthFactor;
                isPinkOrbBig = true;
            }
            else
            {
                // Shrink it back to original size by dividing by the growth factor
                other.transform.localScale /= growthFactor;
                isPinkOrbBig = false;
            }
        }
    }
}
