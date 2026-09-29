using UnityEngine;

public class WandInteraction : MonoBehaviour
{
    [Header("Orange Orb Settings")] [Tooltip("How long it takes for the orange orb to fade completely out")]
    public float fadeDuration = 1.0f;

    [Header("Green Orb Settings")] public Material newGreenMaterial;

    [Header("Pink Orb Settings")] [Tooltip("How much larger the pink orb gets (e.g., 1.5 means 150% of original size)")]
    public float growthFactor = 1.5f;

    [Tooltip("How long it takes for the pink orb to grow or shrink")]
    public float scaleDuration = 0.5f;

    private bool isPinkOrbBig = false;
    private bool isPinkOrbScaling = false; // Prevents spamming the orb while it's mid-animation

    private void OnTriggerEnter(Collider other)
    {
        // 1. Orange Orb: Smooth Fade Away
        if (other.CompareTag("orangeOrb"))
        {
            StartCoroutine(FadeAndDestroy(other.gameObject));
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

        // 3. Pink Orb: Smooth Alternating Grow and Shrink
        else if (other.CompareTag("pinkOrb") && !isPinkOrbScaling)
        {
            Vector3 currentScale = other.transform.localScale;
            Vector3 targetScale;

            if (!isPinkOrbBig)
            {
                targetScale = currentScale * growthFactor;
                isPinkOrbBig = true;
            }
            else
            {
                targetScale = currentScale / growthFactor;
                isPinkOrbBig = false;
            }

            // Start the smooth scaling animation
            StartCoroutine(ScaleOverTime(other.transform, targetScale));
        }
    }

    // Coroutine for the Orange Orb Fade
    private System.Collections.IEnumerator FadeAndDestroy(GameObject target)
    {
        Renderer orbRenderer = target.GetComponent<Renderer>();
        Collider orbCollider = target.GetComponent<Collider>();
        if (orbCollider != null) orbCollider.enabled = false;

        if (orbRenderer != null)
        {
            Material mat = orbRenderer.material;
            Color originalColor = mat.color;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
                yield return null;
            }
        }

        Destroy(target);
    }

    // Coroutine for the Pink Orb Smooth Scale
    private System.Collections.IEnumerator ScaleOverTime(Transform targetTransform, Vector3 targetScale)
    {
        isPinkOrbScaling = true; // Lock the interaction

        Vector3 startingScale = targetTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            // Safeguard in case the orb is destroyed mid-animation by something else
            if (targetTransform == null) yield break;

            elapsedTime += Time.deltaTime;

            // Smoothly blend between starting scale and target scale
            targetTransform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / scaleDuration);

            yield return null;
        }
    }
}