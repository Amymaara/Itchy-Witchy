using UnityEngine;
using TMPro;
using System.Collections.Generic;

//Title: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios
//Author: SasquatchB Studios
//Date Created: 18 Feb 2021
//Date Accessed: 18 August 2025
//Code Version: 1
//Availability: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios
// I used this as a baseline but had to adapt it a bit since we used a 3d space for the market to disappear when textbox appears

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;

    private string p;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        // if theres nothing in queue
        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                //start conversation
                StartConversation(dialogueText);
            }

            else
            {
                //end conversation
                EndConversation();
                return;
            }
        }

        // if theres something in queue
        p = paragraphs.Dequeue();

        // update conversation text
        NPCDialogueText.text = p;

        //update conversationEnded bool
        if (paragraphs.Count == 0)
        {
           conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        //activate gameObject
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        //update speaker name
        NPCNameText.text = dialogueText.speakerName;

        //add dialogue text into queue

        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        // clear the queue

        //return bool to false
        conversationEnded = false;

        //deactivate game object

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
