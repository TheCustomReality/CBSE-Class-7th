using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ToothGrabListener : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isPlaced = false;  

    private void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (interactable == null)
        {
            Debug.LogError("ToothGrabListener requires an XRGrabInteractable component.");
            return;
        }

        // Store the initial position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Listen for grab and drop events
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isPlaced)
        {
            Debug.Log("Tooth already placed: " + gameObject.name);
            return;
        }

        // Allow grabbing but reset if it is released before placement
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (isPlaced) return; 

        // If the tooth is dropped but NOT placed, respawn it
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Small delay before respawning

        if (isPlaced) yield break;

        Debug.Log("Tooth dropped! Respawning: " + gameObject.name);

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void SetPlaced(bool placed)
    {
        isPlaced = placed;
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
            interactable.selectExited.RemoveListener(OnSelectExited);
        }
    }
}
