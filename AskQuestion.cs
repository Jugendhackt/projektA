using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AskQuestion : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Question[] questions;

    Question question;

    public void AskNewQuestion()
    {
        if (canvasGroup.alpha > 0)
            return;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        question = questions[Random.Range(0, questions.Length)];
        text.text = question.question;
    }
}
