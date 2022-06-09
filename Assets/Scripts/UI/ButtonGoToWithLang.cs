using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGoToWithLang : ButtonClickable
{
    [Tooltip("ID of the scene to load when this button is clicked")]
    public int SceneId;

    [SerializeField]
    [Tooltip("Clicking this button will set the language to this one")]
    private LangMode lang;

    protected override void OnClick()
    {
        // Set the language
        CollectableItem.langMode = lang;

        // Go to the scene
        SceneManager.LoadScene(SceneId);
    }
}
