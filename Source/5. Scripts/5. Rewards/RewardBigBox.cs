using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardBigBox : MonoBehaviour
{
    public event UnityAction RestartedAnimationWait;

    public void RestartAnimationWait() => RestartedAnimationWait?.Invoke();
}