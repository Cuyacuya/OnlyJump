using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEvent : MonoBehaviour
{
    [SerializeField] float delay = 0.1f; // 각 글자 간의 딜레이 시간
    private string fullText; // 표시할 전체 텍스트
    private TextMeshProUGUI text;
    private string currentText = ""; // 현재까지 표시된 텍스트
    private float timer = 0f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        fullText = text.text;
        text.text = ""; // 시작할 때 텍스트를 지워둠
    }

    private void Update()
    {
        if (currentText.Length < fullText.Length)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                currentText = fullText.Substring(0, currentText.Length + 1);
                text.text = currentText;
                timer = 0f;
            }
        }
    }

    IEnumerator TypingText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            text.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
