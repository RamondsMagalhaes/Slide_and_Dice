using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    Queue<Dialogue> sentencesQueue;
    public TextMeshProUGUI dialogueText;
    public RawImage dialogueImage;
    public Animator boxAnimator;
    public bool isDialogOver;

    void Awake()
    {
        sentencesQueue = new Queue<Dialogue>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        boxAnimator.SetBool("isOpen", true);
        isDialogOver = false;
        sentencesQueue.Clear();
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            Dialogue newDialogue = new Dialogue(dialogue.sentences[i], dialogue.images[i]);
            sentencesQueue.Enqueue(newDialogue);
        }
        //foreach (Dialogue sentence in sentencesQueue)
        //{
        //    sentencesQueue.Enqueue(sentence);
        //}
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        Debug.Log("SentencesQueue count: " + sentencesQueue.Count);
        if (sentencesQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }
        Dialogue sentence = sentencesQueue.Dequeue();
        dialogueImage.texture = sentence.images[0];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentences[0]));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        isDialogOver = true;
        boxAnimator.SetBool("isOpen", false);
    }
}
