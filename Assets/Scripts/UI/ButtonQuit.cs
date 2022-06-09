using UnityEngine;

public class ButtonQuit : ButtonClickable
{
    // Start is called before the first frame update
    protected override void OnClick()
    {
        // Quit
        Debug.Log("Bye :)");

        Application.Quit();
    }
}
