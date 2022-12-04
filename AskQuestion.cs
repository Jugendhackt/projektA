using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    Slider clock;

    [SerializeField]
    AudioSource tickTock;

    [SerializeField]
    AudioClip correct, wrong;

    private void Update()
    {
        if(canvasGroup.alpha > 0)
        {
            clock.value += (Time.deltaTime / 5f);
            Debug.Log(Time.deltaTime);
            if(clock.value >= 1)
            {
                player.ReloadLevel();
            }
        }
    }

    public void AskNewQuestion()
    {
        if (canvasGroup.alpha > 0)
            return;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        clock.value = 0;

        question = questions[Random.Range(0, questions.Length)];
        text.text = question.question;
        tickTock.Play();
    }

    public void AnswerQuestion(bool answer)
    {
        tickTock.Stop();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if(answer == question.isTrue)
        {
            AudioSource.PlayClipAtPoint(correct, player.transform.position);
            StartCoroutine(player.Respawn());
        }
        else
        {
            AudioSource.PlayClipAtPoint(wrong, player.transform.position);
            StartCoroutine(player.ReloadLevel());
        }
    }
}
