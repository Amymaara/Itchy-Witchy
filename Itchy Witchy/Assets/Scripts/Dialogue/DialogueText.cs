using UnityEngine;

//Title: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios
//Author: SasquatchB Studios
//Date Created: 18 Feb 2021
//Date Accessed: 18 August 2025
//Code Version: 1
//Availability: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios


[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject


{
    public string speakerName;

    [TextArea(5, 10)]
    public string[] paragraphs;
}
