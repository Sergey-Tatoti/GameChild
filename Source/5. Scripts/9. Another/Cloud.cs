using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cloud : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _startPositionX;
    private float _endPositionX;

    public event UnityAction EndedMove;

    public void Activate(float endPositionX)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPositionX = transform.position.x;
        _endPositionX = endPositionX;
    }

    public void Generate(Sprite sprite, float durationMove, float delayMove, float scale, float positionY)
    {
        _spriteRenderer.sprite = sprite;
        transform.position = new Vector3(_startPositionX, positionY, transform.position.z);
        transform.localScale = new Vector3(scale, scale, scale);
        gameObject.SetActive(true);

        StartCoroutine(UseMove(delayMove, durationMove));
    }

    private IEnumerator UseMove(float delayMove, float durationMove)
    {
        yield return new WaitForSeconds(delayMove);

        transform.DOMoveX(_endPositionX, durationMove).SetEase(Ease.Linear).OnComplete(() => StopMove());
    }

    private void StopMove()
    {
        gameObject.SetActive(false);
        EndedMove?.Invoke();
    }
}