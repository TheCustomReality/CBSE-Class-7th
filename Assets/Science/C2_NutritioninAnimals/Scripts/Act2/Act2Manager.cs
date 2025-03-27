using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2Manager : MonoBehaviour
{
    public Ath3na ath3na;
    public Dialogue startDialogue;
    public Dialogue endDialogue;

    [SerializeField]public static int CorrectPlacement = 0;
    
    
    private void OnEnable()
    {
        //Materialize the objects in scean
    }
    

    public IEnumerator StartAct2()
    {
        ath3na.Speak(startDialogue);
        yield return new WaitForSeconds(startDialogue._dialogAudioClip.length);
        
        //Start the activity here
        yield return StartCoroutine(coreLoopAct2());
        
        ath3na.Speak(endDialogue);
        yield return new WaitForSeconds(endDialogue._dialogAudioClip.length );

    }

    private IEnumerator coreLoopAct2()
    {
        yield return new WaitUntil(() => CorrectPlacement >= 6);
    }
}
