using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGoTo : ButtonClickable
{

    [Tooltip("ID of the scene to load when this button is clicked")]
    public int SceneId;

    protected override void OnClick()
    {
        SceneManager.LoadScene(SceneId);
    }

}
