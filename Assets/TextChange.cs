using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextChange : MonoBehaviour
{
    public int textIndex = 0;
    public TMP_Text text;

    public void SetText(string textToImport)
    {
        text.text = textToImport;
    }
}
