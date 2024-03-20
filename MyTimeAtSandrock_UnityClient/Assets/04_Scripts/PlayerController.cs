using Google.Protobuf.Protocol;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : GameObjectController
{
    public CharacterController characterController;

    protected override void UpdateController()
    {
        base.UpdateController();

        UpdatePosition();
    }

    private float _timer;
    private void UpdatePosition()
    {
        _timer += Time.deltaTime;
        if (_timer > 1.0f)
        {
            Position = transform.position;
            Rotation = transform.rotation.eulerAngles;

            CheckUpdatedFlag();
        }
    }

    private void CheckUpdatedFlag()
    {
        if (_dirty)
        {
            C_Move movePacket = new C_Move();
            movePacket.ObjectData = this.ObjectData;
            Managers.NetworkManager.Send(movePacket);
            _dirty = false;
        }
    }

    public void Move(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            return;
        }

        characterController.Move(dir);
    }
}
