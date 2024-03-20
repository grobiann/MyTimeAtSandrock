using UnityEngine;

public class IngameSceneController : MonoBehaviour
{
    private void Start()
    {
        // 캐릭터 로드
        var player = Managers.ObjectManager.CreatePlayer();

        // TODO: 건물 로드
    }
}