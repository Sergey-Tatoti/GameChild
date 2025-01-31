using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    private Sequence _moveSequence;
    private GameObject _character;
    private List<Vector3> _directions;
    private Vector3 _fullRotate = new Vector3(0, 180, 0);

    private float _durationMove;
    private float _stepHorizontal;
    private float _stepVertical;
    private bool _startFlipX;

    public event UnityAction StepEnded;
    public event UnityAction<Vector3> ChangedTargetMove;

    public void StopMove() { _moveSequence.Kill(); _character.gameObject.transform.localEulerAngles = Vector3.zero; }

    public void SetValue(float stepHorizontal, float stepVertical, float durationMove, bool isRightDirection, GameObject character)
    {
        _character = character;
        _startFlipX = !isRightDirection;
        _durationMove = durationMove;
        _stepHorizontal = stepHorizontal;
        _stepVertical = stepVertical;

        _character.gameObject.transform.localEulerAngles = _startFlipX ? _fullRotate : Vector3.zero;
    }

    public IEnumerator Move(List<Vector3> directions)
    {
        float currentStep;

        if(directions == null)
            directions = new List<Vector3>(_directions);
        else
            _directions = new List<Vector3>(directions);

        for (int i = 0; i < directions.Count; i++)
        {
            currentStep = directions[i].x == 0? _stepVertical : _stepHorizontal;
            Vector2 target = transform.position + (directions[i] * currentStep);

            TryFlip(directions[i].x);
            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOMove(target, _durationMove));
            _directions.RemoveAt(0);

            ChangedTargetMove?.Invoke(target);

            yield return new WaitForSeconds(_durationMove);
        }

        _moveSequence.Kill();
        StepEnded?.Invoke();
    }

    private void TryFlip(float direction)
    {
        if (direction == 0) { return; }

        _character.gameObject.transform.localEulerAngles = direction > 0 ? Vector3.zero : _fullRotate;
    }
}