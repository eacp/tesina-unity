using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I18nUiText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the proper text to show
        string text;
        switch(CollectableItem.langMode)
        {
            case LangMode.Spanish:
                text = esText;
                break;
            case LangMode.English:
                text = enText;
                break;
            case LangMode.Portuguese:
                text = ptText;
                break;
            default:
                text = enText;
                break;
        }

        var textBlock = GetComponent<UnityEngine.UI.Text>();

        if (text != null) textBlock.text = text;
    }

    [SerializeField]
    [Tooltip("The text in english.")]
    private string enText;
    [SerializeField]
    [Tooltip("The text in spanish.")]
    private string esText;
    [SerializeField]
    [Tooltip("The text in portuguese.")]
    private string ptText;

}
