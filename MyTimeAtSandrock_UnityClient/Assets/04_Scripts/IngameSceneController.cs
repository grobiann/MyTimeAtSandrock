using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameSceneController : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Additive);

        // 캐릭터 로드
        var player = Managers.ObjectManager.CreatePlayer();

        // TODO: 건물 로드
    }
}
