using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act3Manager : MonoBehaviour
{
    public Ath3na ath3na;

    public Dialogue startDialogue;

    void Start()
    {
        StartCoroutine(PlayAct3Sequence());
    }

    [System.Serializable]
    public class OrganHighlight
    {
        public GameObject organ;
        public Dialogue dialogue;
    }

    public List<OrganHighlight> organHighlights = new List<OrganHighlight>();

    public IEnumerator PlayAct3Sequence()
    {
        // Play intro narration
        ath3na.Speak(startDialogue);
        yield return new WaitForSeconds(startDialogue._dialogAudioClip.length + 3f);

        // Iterate through each organ
        foreach (var organData in organHighlights)
        {
            OrganHighlighter highlighter = organData.organ.GetComponent<OrganHighlighter>();

            // Debug log to print organ name
            Debug.Log("Highlighting Organ: " + organData.organ.name);

            if (highlighter != null)
            {
                yield return StartCoroutine(HighlightAndSpeak(highlighter, organData.dialogue));
            }
            else
            {
                Debug.LogWarning("No OrganHighlighter found on " + organData.organ.name);
            }
        }

        Debug.Log("Act 3 Completed.");
    }

    private IEnumerator HighlightAndSpeak(OrganHighlighter highlighter, Dialogue dialogue)
    {
        ath3na.Speak(dialogue);
        yield return StartCoroutine(highlighter.HighlightOrgan());
        yield return new WaitForSeconds(dialogue._dialogAudioClip.length + 3f);
    }
}
