using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public Text score_text;
    public int  score_point;

    private void Start()
    {
        score_text = GameObject.Find("Score_Text").GetComponent<Text>();
    }

    public void Score_Update()
    {
        score_point += 7;

        score_text.text = "Score: " + score_point.ToString();
    }
}
