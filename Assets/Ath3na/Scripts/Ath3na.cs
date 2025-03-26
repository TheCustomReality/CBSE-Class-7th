using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Ath3na : MonoBehaviour
{
    
    public AudioSource audioSource;
    public Animator animator; 
    private bool speaking = false;
    [SerializeField] private Ath3naSpwaner _spwaner;
    public bool test = false ;

    private void Update()
    {
        if (test)
        {
            Despawn();
            test = false;
        }
    }
    public void Speak(Dialogue dialogue)
    {
        if (speaking)
        {
            return;
        }
        else
        {
            speaking = true;
            audioSource.clip = dialogue._dialogAudioClip;
            audioSource.Play();
            //Display Captions
            //working on it.
            
            //Play Animation
            if (animator)
            {
                string trigger = "talk";
                int randomInt = UnityEngine.Random.Range(1, 2 + 1);
                trigger += randomInt.ToString();
                animator.SetTrigger(trigger);
                StartCoroutine(waitforSpeaking(dialogue._dialogAudioClip.length));
            }
        }
    }

    private IEnumerator waitforSpeaking(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("Idel");
        speaking= false;
    }

    public void MoveAth3na(Transform transform)
    {
        _spwaner.DespawnObject();
        StartCoroutine(waitTillDespawn(transform));
    }

    private IEnumerator waitTillDespawn(Transform transform)
    {
        yield return new WaitForSeconds(_spwaner.transitionTime);
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
        _spwaner.SpawnObject();
    }
    
    public void Spawn()
    {
        _spwaner.SpawnObject();
    }

    public void Despawn()
    {
        _spwaner.DespawnObject();
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.SetTrigger(animationName);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }
    }
}
