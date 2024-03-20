using UnityEngine;

public class InputController : MonoBehaviour
{
    // HACK: key 관리하는 클래스 만들기
    public KeyCode front = KeyCode.W;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;
    public KeyCode back = KeyCode.S;

    public void Update()
    {
        var moveDir = Vector3.zero;
        if(Input.GetKey(front))
        {
            moveDir += Vector3.forward;
        }
        if(Input.GetKey(back))
        {
            moveDir -= Vector3.forward;
        }
        if (Input.GetKey(right))
        {
            moveDir += Vector3.right;
        }
        if (Input.GetKey(left))
        {
            moveDir -= Vector3.right;
        }

        var player = Managers.ObjectManager.MyPlayer;
        if (player == null)
            return;

        player.Move(moveDir);
    }

    // 키보드 입력
    void GetDirInput()
    {
    }
}
