using UnityEngine;
using System.Collections;

public class KeyboardManager : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    public delegate void KeyBoardInput(string text);
    public event KeyBoardInput OnKeyBoardInputDone;
    private bool eventCalled = false;
    // Use this for initialization
    void Start()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.done)
        {
            if (!eventCalled)
                KeyboardDone();

        }

    }

    public void ShowKeyboard(TouchScreenKeyboard kb)
    {
        kb = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void HideKeyboard(TouchScreenKeyboard kb)
    {
        if (TouchScreenKeyboard.visible)
            kb.active = false;
    }

    private void KeyboardDone()
    {
        if (OnKeyBoardInputDone != null)
        {
            OnKeyBoardInputDone(keyboard.text);
            eventCalled = true;
        }
    }
}
