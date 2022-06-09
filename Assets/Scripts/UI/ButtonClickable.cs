using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
    Base class for all clickable buttons
*/
public abstract class ButtonClickable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        // Add the method
        button.onClick.AddListener(OnClick);

    }

    // Callback when the button is clicked
    protected abstract void OnClick();
}
