using UnityEditor;
using UnityEditor.SceneManagement;

public class CustomMenu
{
    [MenuItem("My Time At Sandrock/Play Scene - Intro")]
    public static void Play()
    {
        EditorSceneManager.OpenScene("Assets/00_Scenes/Intro.unity");
        EditorApplication.EnterPlaymode();
    }
}