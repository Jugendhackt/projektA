using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    TextMeshProUGUI text;

    private void Update()
    {
        text.text = (int)player.position.x + "m";
    }
}
