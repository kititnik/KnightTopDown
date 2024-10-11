using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public TMP_Text TextOnTop;
    public TMP_Text RichTextOnTop;
    [SerializeField] private TMP_Text notificationsText;

    public void SetText(TMP_Text text, string info, float interval)
    {
        StartCoroutine(SetTextOnTopCor(text, info, interval));
    }

    public void SetText(TMP_Text text, string info)
    {
        text.text = info;
    }

    public void RemoveText(TMP_Text text, string info)
    {
        if(text.text == info) text.text = "";
    }

    public void AddNoification(string text, float interval)
    {
        StartCoroutine(AddNoificationCor(text, interval));
    }

    public IEnumerator AddNoificationCor(string text, float interval)
    {
        notificationsText.text += text + "\n";
        yield return new WaitForSeconds(interval);
        string fullText = notificationsText.text;
        int itemIndex = fullText.LastIndexOf(text);
        if (itemIndex >= 0)
        {
            fullText = fullText.Remove(itemIndex, text.Length + 1); // +1 для символа новой строки
        }

        notificationsText.text = fullText;
    }

    private IEnumerator SetTextOnTopCor(TMP_Text text, string info, float interval)
    {
        text.text = info;
        yield return new WaitForSeconds(interval);
        if(text.text == info) text.text = "";
    }
}
