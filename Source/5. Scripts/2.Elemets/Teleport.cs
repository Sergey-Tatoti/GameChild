using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Tooltip("ѕозици€ где окажетс€ игрок при телепорте")][SerializeField] private Vector3 _positionSpawn;
    [Tooltip("“елепорт на который нужно переместитс€")][SerializeField] private Teleport _anotherTeleport;

    private bool _isLock;

    public Vector3 PositionSpawn => _positionSpawn;
    public Vector3 AnotherTeleportPositionSpawn => _anotherTeleport.PositionSpawn;
    public bool IsLock => _isLock;

    public void UseLockAnotherTeleport()
    {
        if (!_isLock)
            _anotherTeleport.UseLock(true);
    }

    public void UseLock(bool isLock) => _isLock = isLock;
}