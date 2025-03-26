using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1Manager : MonoBehaviour
{ 
    public Ath3na ath3na;  

    public Dialogue startDialogue;

    [System.Serializable]
    public class ToothHighlight
    {
        public GroupTeethHighlighter highlighter;
        public Dialogue dialogue;
    }

    public List<ToothHighlight> toothHighlights = new List<ToothHighlight>();

    private void Start()
    {
        StartCoroutine(PlayAct1Sequence());
    }

    private IEnumerator PlayAct1Sequence()
    {
        // Play intro narration
        ath3na.Speak(startDialogue);
        yield return new WaitForSeconds(startDialogue._dialogAudioClip.length);

        // Go through each tooth group dynamically
        foreach (var tooth in toothHighlights)
        {
            yield return StartCoroutine(HighlightAndSpeak(tooth.highlighter, tooth.dialogue));
        }

    }

    private IEnumerator HighlightAndSpeak(GroupTeethHighlighter highlighter, Dialogue dialogue)
    {
        ath3na.Speak(dialogue); // Start speaking
        yield return StartCoroutine(highlighter.HighlightTeeth(dialogue._dialogAudioClip.length)); // Highlight teeth while talking
    }
}
