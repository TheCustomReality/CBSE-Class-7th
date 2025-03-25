using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Dialogue startDialogue;
    public Dialogue endDialogue;
    public Ath3na athena;
    public GroupTeethManager groupTeethManager;

    public void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Play start dialogue first
        yield return StartCoroutine(AudioPlay(startDialogue));

        // Play the teeth highlight sequence
        yield return StartCoroutine(groupTeethManager.HighlightTeethSequence());

        // Play the end dialogue
        yield return StartCoroutine(AudioPlay(endDialogue));
    }

    private IEnumerator AudioPlay(Dialogue dialogue)
    {
        athena.Speak(dialogue);
        yield return new WaitForSeconds(dialogue._dialogAudioClip.length);
    }
}
