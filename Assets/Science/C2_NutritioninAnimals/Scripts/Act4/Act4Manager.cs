using System.Collections;
using UnityEngine;

public class Act4Manager : MonoBehaviour
{
    public Ath3na ath3na;
    public Dialogue startDialogue;
    public Dialogue endDialogue;
    public Dialogue[] organDialogues; // Array for organ placement prompts
    
    [SerializeField] public static int CorrectPlacement = 0;
    public int totalOrgans = 8; // Total organs to place

    private void OnEnable()
    {
        CorrectPlacement = 0; // Reset at start
    }

    public IEnumerator StartAct4()
    {
        ath3na.Speak(startDialogue);
        yield return new WaitForSeconds(startDialogue._dialogAudioClip.length);

        // Start the activity
        yield return StartCoroutine(coreLoopAct4());
        
        ath3na.Speak(endDialogue);
        yield return new WaitForSeconds(endDialogue._dialogAudioClip.length);
    }

    private IEnumerator coreLoopAct4()
    {
        for (int i = 0; i < totalOrgans; i++)
        {
            ath3na.Speak(organDialogues[i]);
            yield return new WaitForSeconds(organDialogues[i]._dialogAudioClip.length);
            
            yield return new WaitUntil(() => CorrectPlacement > i);
        }
    }
}
