using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public TMP_Text TextOnTop;
    public TMP_Text RichTextOnTop;

    public void SetText(TMP_Text text, string info, float interval)
    {
        StartCoroutine(SetTextOnTopCor(text, info, interval));
    }

    public IEnumerator SetTextOnTopCor(TMP_Text text, string info, float interval)
    {
        text.text = info;
        yield return new WaitForSeconds(interval);
        text.text = "";
    }
}
