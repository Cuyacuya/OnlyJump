using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkAnim : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(.5f);
            text.text = "TAB TO PLAY";
            yield return new WaitForSeconds(.5f);
        }
    }
}
