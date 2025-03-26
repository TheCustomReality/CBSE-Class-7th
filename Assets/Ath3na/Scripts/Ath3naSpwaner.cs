using UnityEngine;
using System.Collections;

public class Ath3naSpwaner : MonoBehaviour
{
    public Material customMaterial; // Assign this in the inspector
    public float transitionTime = 1f; // Speed of transition
    private float currentValue;
    void Start()
    {
        if (customMaterial != null)
        { 
            currentValue = customMaterial.GetFloat("_spawn_y");
        }
    }

    public void SpawnObject()
    {
        StartCoroutine(Spawn());
    }

    public void DespawnObject()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Spawn()
    {
        float startValue = customMaterial.GetFloat("_spawn_y");
        float targetValue = 0f;
        float elapsedTime = 0f;
    
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime; // Normalized time (0 to 1)
        
            // Linear interpolation from start to target
            float currentValue = Mathf.Lerp(startValue, targetValue, t);
        
            // Alternative: smoother transition with ease in/out
            // float currentValue = Mathf.SmoothStep(startValue, targetValue, t);
        
            customMaterial.SetFloat("_spawn_y", currentValue);
            yield return null; // Wait for next frame
        }
    
        // Ensure final value is exactly the target
        customMaterial.SetFloat("_spawn_y", targetValue);
    }

    
    private IEnumerator Despawn()
    {
        float startValue = currentValue;
        float targetValue = 1f;
        float elapsedTime = 0f;
    
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime; 
            currentValue = Mathf.Lerp(startValue, targetValue, t);
            customMaterial.SetFloat("_spawn_y", currentValue);
            yield return null;
        }
        
        currentValue = targetValue;
        customMaterial.SetFloat("_spawn_y", targetValue);
    }

    
}