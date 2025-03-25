using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Ath3na : MonoBehaviour
{
    
    public AudioSource audioSource;
    public Animator animator;
    public TextMeshProUGUI captionText; // For UI caption display
    public float captionDisplayTime = 5f; // Duration to show text
    bool active = false;
    public void Speak(Dialogue dialogue)
    {
        audioSource.clip = dialogue._dialogAudioClip;
        audioSource.Play();
        //Dispy caption
        
    }

    private System.Collections.IEnumerator DisplayCaption(string text)
    {
        captionText.text = text;
        yield return new WaitForSeconds(captionDisplayTime);
        captionText.text = ""; // Clear text after duration
    }

    public void Spawn(Transform spawnLocation)
    {
        if (!active)
        {
            transform.position = spawnLocation.position;
            transform.rotation = spawnLocation.rotation;
            gameObject.SetActive(true);
            active = true;
            Debug.Log("Ath3na has spawned at the target location.");
        }
    }

    public void Despawn()
    {
        if (active)
        {
            gameObject.SetActive(false);
            active = false;
            Debug.Log("Ath3na has despawned.");
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.SetTrigger(animationName);
            Debug.Log($"Playing animation: {animationName}");
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }
    }
}
