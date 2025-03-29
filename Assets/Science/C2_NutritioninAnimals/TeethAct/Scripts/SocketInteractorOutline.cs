using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SocketInteractorOutline : MonoBehaviour
{
    public Material correctOutlineMaterial;
    public Material incorrectOutlineMaterial;
    public string correctTag; // Tag of the correct object
    public AudioSource correctAudio;
    public AudioSource incorrectAudio;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    private void Awake()
    {
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        if (socketInteractor == null)
        {
            Debug.LogError("XRSocketInteractor component is missing on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectPlaced);
            socketInteractor.selectExited.AddListener(OnObjectRemoved);
        }
    }

    private void OnDisable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
            socketInteractor.selectExited.RemoveListener(OnObjectRemoved);
        }
    }
    
    private IEnumerator LockOrganInSocket(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable, GameObject socket)
    {
        yield return new WaitForSeconds(0.2f);

        interactable.transform.SetParent(socket.transform);
        interactable.enabled = false;

        Rigidbody rb = interactable.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (args.interactableObject == null)
        {
            Debug.LogWarning("Placed object is null.");
            return;
        }

        GameObject placedObject = args.interactableObject.transform.gameObject;
        ToothGrabListener toothGrabListener = placedObject.GetComponent<ToothGrabListener>();

        MeshRenderer objectRenderer = placedObject.GetComponent<MeshRenderer>();

        if (toothGrabListener != null)
        {
            toothGrabListener.SetPlaced(true); // ✅ Mark tooth as placed
        }

        if (objectRenderer == null)
        {
            Debug.LogWarning("MeshRenderer is missing on " + placedObject.name);
            return;
        };
        // Determine outline color based on tag
        if (placedObject.CompareTag(correctTag))
        {
            if (correctAudio)
            {
                //This is correct attempt
                Act2Manager.CorrectPlacement++;
                StatsManager._StatsManager.score += 10;
                correctAudio.Play();
                //Disable XRGrabInteractable compontnet
                // XRGrabInteractable interactable = placedObject.GetComponent<XRGrabInteractable>();
                // if (interactable)
                // {
                //     Debug.Log("Disabled");
                //     interactable.enabled = false;
                // }
            }
        }
        else
        {
            if (incorrectAudio)
            {
                //This is incorrect attempt
                StatsManager._StatsManager.score -= 2;
                StatsManager._StatsManager.incorrectPlacementAct2++;
                incorrectAudio.Play();
            }
        }
        
        Material outlineMaterial= placedObject.CompareTag(correctTag) ? correctOutlineMaterial : incorrectOutlineMaterial;

        if (outlineMaterial == null)
        {
            Debug.LogError("Outline material is not assigned in the Inspector.");
            return;
        }

        // Add the outline material
        Material[] originalMaterials = objectRenderer.materials;
        Material[] newMaterials = new Material[originalMaterials.Length + 1];

        for (int i = 0; i < originalMaterials.Length; i++)
        {
            newMaterials[i] = originalMaterials[i];
        }

        newMaterials[originalMaterials.Length] = outlineMaterial;
        objectRenderer.materials = newMaterials;

        Debug.Log("Outline added to " + placedObject.name);
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (args.interactableObject == null)
        {
            Debug.LogWarning("Removed object is null.");
            return;
        }

        GameObject removedObject = args.interactableObject.transform.gameObject;
        MeshRenderer objectRenderer = removedObject.GetComponent<MeshRenderer>();

        if (objectRenderer == null)
        {
            Debug.LogWarning("MeshRenderer missing on removed object: " + removedObject.name);
            return;
        }

        // Restore original materials by removing the last added outline material
        Material[] currentMaterials = objectRenderer.materials;
        if (currentMaterials.Length > 1)
        {
            Material[] newMaterials = new Material[currentMaterials.Length - 1];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = currentMaterials[i];
            }
            objectRenderer.materials = newMaterials;
            Debug.Log("Outline removed from " + removedObject.name);
        }
    }
}
