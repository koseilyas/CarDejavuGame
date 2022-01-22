using TMPro;
using UnityEngine;

public class LevelEditorLogger
{
    private static TMP_Text _logText;

    public LevelEditorLogger(TMP_Text logText)
    {
        _logText = logText;
    }

    public static void Log(string s, bool isWarning)
    {
        _logText.color = isWarning ? Color.red : Color.white;
        _logText.SetText(s);
    }
}
