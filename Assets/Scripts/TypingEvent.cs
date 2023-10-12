using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEvent : MonoBehaviour
{
    [SerializeField] float delay = 0.1f; // �� ���� ���� ������ �ð�
    private string fullText; // ǥ���� ��ü �ؽ�Ʈ
    private TextMeshProUGUI text;
    private string currentText = ""; // ������� ǥ�õ� �ؽ�Ʈ
    private float timer = 0f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        fullText = text.text;
        text.text = ""; // ������ �� �ؽ�Ʈ�� ������
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
