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

    [SerializeField]
    PlayerMovement player;

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

    public void AnswerQuestion(bool answer)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
        if(answer == question.isTrue)
        {
            player.Respawn();
        }
        else
        {
            player.ReloadLevel();
        }
    }
}
