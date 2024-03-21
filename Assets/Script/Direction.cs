using UnityEngine;

public class Direction : MonoBehaviour
{
    public GameObject circleObject;
    private SpriteRenderer[] spriteRenderers;
    private Color endColor;
    public float colorChangeTime;
    private int currentIndex = 0; // Track the current index of the child object

    void Start()
    {
        if (circleObject != null)
        {
            spriteRenderers = circleObject.GetComponentsInChildren<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("circleObject is not assigned!");
        }

        endColor = Color.red;
    }

    void Update()
    {
        if (spriteRenderers != null && spriteRenderers.Length > 0)
        {
            // Check if currentIndex is within bounds
            if (currentIndex < spriteRenderers.Length)
            {
                // Get the current SpriteRenderer
                SpriteRenderer currentRenderer = spriteRenderers[currentIndex];

                // Change color of the current SpriteRenderer
                currentRenderer.color = Color.Lerp(currentRenderer.color, endColor, colorChangeTime * Time.fixedDeltaTime);

                // Check if the color change is almost complete for the current SpriteRenderer
                if (currentRenderer.color == endColor || Vector4.Distance(currentRenderer.color, endColor) < 0.01f)
                {
                    // Move to the next child object
                    currentIndex++;
                }
            }
            else
            {
                // Reset currentIndex to restart the color filling process
                currentIndex = 0;
            }
        }
        else
        {
            Debug.LogWarning("No SpriteRenderers found on child GameObjects of circleObject!");
        }
    }
}
