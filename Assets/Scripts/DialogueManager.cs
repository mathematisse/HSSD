using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public static DialogueManager instance;
    private Queue<string> sentences;
    private int isDisplaying;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        isDisplaying = 1;
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
    }

    void EndDialogue()
    {
        isDisplaying = 0;
        animator.SetBool("IsOpen", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isDisplaying == 1) {
            DisplayNextSentence();
        }
    }
}
