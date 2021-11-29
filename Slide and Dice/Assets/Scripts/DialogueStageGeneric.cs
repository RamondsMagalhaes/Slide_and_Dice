using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStageGeneric : MonoBehaviour
{
    DialogueTrigger trigger;
    void Start()
    {
        trigger = FindObjectOfType<DialogueTrigger>();
    }

    public void StageDialogue()
    {
        trigger.TriggerDialogue();
    }

}
