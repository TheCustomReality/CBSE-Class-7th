using UnityEngine;
using UnityEngine.UI;

public class fps : MonoBehaviour
{
    public Text fpsText;// Reference to the TextMeshProUGUI component
    private float pollingTime = 0.5f; // Time interval for FPS updates
    private float time;
    private int frameCount;

    void Update()
    {
        // Accumulate time and frame count
        time += Time.deltaTime;
        frameCount++;

        // Update FPS display at intervals specified by pollingTime
        if (time >= pollingTime)
        {
            int fps = Mathf.RoundToInt(frameCount / time); // Calculate FPS
            fpsText.text = $"  FPS: {fps}"; // Display FPS in the text component
            time = 0; // Reset time
            frameCount = 0; // Reset frame count
        }
    }
}
