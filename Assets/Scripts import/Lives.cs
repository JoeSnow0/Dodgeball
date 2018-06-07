using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Lives : MonoBehaviour {
    public float spriteWidth;
    private Image m_LivesImg;

	// Use this for initialization
	void Awake () {
        m_LivesImg = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        m_LivesImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spriteWidth * Paddle.lives);
	}
}
