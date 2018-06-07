using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour {
    private Text m_ScoreText;

	// Use this for initialization
	void Awake () {
        m_ScoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        m_ScoreText.text = "SCORE: " + Paddle.score;
	}
}
