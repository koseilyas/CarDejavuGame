#if UNITY_EDITOR
using TMPro;
using UnityEngine;

public class EditorLogger
{
    private static TMP_Text _logText;

    public EditorLogger(TMP_Text logText)
    {
        _logText = logText;
    }

    public static void Log(string s, bool isWarning = false)
    {
        _logText.color = isWarning ? Color.red : Color.white;
        _logText.SetText(s);
    }
}
#endif
