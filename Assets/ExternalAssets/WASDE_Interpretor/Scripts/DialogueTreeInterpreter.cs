using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
[System.Serializable]
public class DialogueCallback : UnityEvent<string, int> { }
public class DialogueTreeInterpreter : MonoBehaviour
{
    public static DialogueTree currentlyPlaying;
    public static int nowOn;
    public static string curFileName;
    public static DialogueUIManager dum;
    public static DialogueCallback DialogueTreeStarted = new DialogueCallback();
    public static DialogueCallback NewDialogueStarted = new DialogueCallback();
    public static DialogueCallback DialogueTreeEnded = new DialogueCallback();
    private void Awake()
    {
        dum = GetComponent<DialogueUIManager>();
    }
    public static void StartDialogue(TextAsset t) {
        currentlyPlaying = JsonConvert.DeserializeObject<DialogueTree>(t.text);
        curFileName = t.name;
        nowOn = 0;
        moveTo(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void endTree() {
        DialogueData d = currentlyPlaying.dialogues[nowOn];
        DialogueTreeEnded.Invoke(d.title, d.id);
        dum.CloseDisplay();
    }
    public static DialogueData moveTo(int id) {
        if (id == -1) {
            endTree();
            return null;
        }
        if (id >= 0 && id < currentlyPlaying.dialogues.Length)
        {
            
            DialogueData d = currentlyPlaying.dialogues[id];
            print(d.charIDs[0]);
            if (id == 0)
            {
                DialogueTreeStarted.Invoke(d.title,d.id);
            }
            NewDialogueStarted.Invoke(d.title, d.id);
            string audioPath = Path.Combine(SceneManager.GetActiveScene().name, curFileName, (d.title));
            //print(audioPath);
            if (Resources.Load<AudioClip>(audioPath) != null)
            {
                Resources.Load<AudioClip>(audioPath);
                //Play Audio
            }
            else {
                Debug.LogWarning("Dialogue Flow tried to play audio at Assets\\Resources" + audioPath + " which does not exist. Defaulting to no Audio.");
            }
            dum.UpdateDisplay(d);
            nowOn = id;
            return d;
        }
        else
        {
            Debug.LogError("Dialogue Flow tried to move to dialogue id " + id + " which does not exist.");
            return null;
        }
        
    }
}
