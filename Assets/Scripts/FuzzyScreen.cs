using UnityEngine;
using UnityEngine.UI;

public class FuzzyScreen : MonoBehaviour
{
    // The duration of the fuzzy screen effect
    public float duration = 2.0f;

    public SceneController sceneController; // Reference to the SceneController script

    // The image component that displays the fuzzy screen
    private Image image;

    // Whether the fuzzy screen is currently active
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the image component of the panel
        image = GetComponent<Image>();

        // Hide the fuzzy screen
        image.enabled = false;
    }

    // Show the fuzzy screen for the specified duration
    public void Show()
    {
        if (!isActive)
        {
            isActive = true;

            // Set the alpha of the image to 1
            Color color = image.color;
            color.a = 1f;
            image.color = color;

            // Display the fuzzy screen
            image.enabled = true;

            // Invoke the hide function after the duration has elapsed
            Invoke("Hide", duration);
        }
    }

    // Hide the fuzzy screen
    public void Hide()
    {
        isActive = false;
        image.enabled = false;
        sceneController.DisplayItems();
    }
}
