using Google.Protobuf.Protocol;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    public GameObjectData ObjectData { get; set; }

    public ECharacterState State { get; set; } = ECharacterState.Idle;
    public Vector3 Position 
    {
        get { return ObjectData.GetPosition(); }
        set
        {
            ObjectData.PosX = value.x;
            ObjectData.PosY = value.y;
            ObjectData.PosZ = value.z;
            _dirty = true;
        }
    }
    public Vector3 Rotation
    {
        get { return new Vector3(ObjectData.RotX, ObjectData.RotY, ObjectData.RotZ); }
        set
        {
            ObjectData.RotX = value.x;
            ObjectData.RotY = value.y;
            ObjectData.RotZ = value.z;
            _dirty = true;
        }
    }

    public float Speed { get; set; }

    private Vector3 _targetPos;
    protected bool _dirty;

    private void Update()
    {
        UpdateController();
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case ECharacterState.Idle:
                break;
            case ECharacterState.Move:
                UpdateMove();
                break;
            default:
                Debug.LogError($"State '{State}' is not defined");
                break;
        }
    }

    public void SyncPosition()
    {
        _targetPos = Position;
        State = ECharacterState.Move;
    }

    public void UpdateMove()
    {
        var dir = _targetPos - transform.position;
        if (dir.magnitude < Speed * Time.deltaTime)
        {
            transform.position = _targetPos;
            State = ECharacterState.Idle;
        }
        else
        {
            transform.position += dir.normalized * Speed * Time.deltaTime;
        }
    }
}

public enum ECharacterState
{
    Idle,
    Move,
}