using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    private const int MinCountClouds = 1;
    private const int MaxCountClouds = 4;
    private const float MinDurationMoveClouds = 10f;
    private const float MaxDurationMoveClouds = 30f;
    private const float MinDelayClouds = 1;
    private const float MaxDelayClouds = 10;
    private const float MinScaleClouds = 0.1f;
    private const float MaxScaleClouds = 1f;

    [SerializeField] private List<Cloud>_clouds;
    [SerializeField] private List<Sprite> _spritesClouds;
    [SerializeField] private float _endPositionX;

    [Header("Мин/Макс позиция по У появления облак")]
    [SerializeField] private float _minYPositionClouds;
    [SerializeField] private float _maxYPositionClouds;
    [Header("Мин/Макс кол-во облак, которые можно увидеть на сцене")]
    [Range(MinCountClouds, MaxCountClouds)][SerializeField] private int _minCountClouds;
    [Range(MinCountClouds, MaxCountClouds)][SerializeField] private int _maxCountClouds;
    [Header("Мин/Макс продолжительность облаков")]
    [Range(MinDurationMoveClouds, MaxDurationMoveClouds)][SerializeField] private float _minDurationMoveClouds;
    [Range(MinDurationMoveClouds, MaxDurationMoveClouds)][SerializeField] private float _maxDurationMoveClouds;
    [Header("Мин/Макс задержка перед появлением нового облака")]
    [Range(MinDelayClouds, MaxDelayClouds)][SerializeField] private float _minDelayClouds;
    [Range(MinDelayClouds, MaxDelayClouds)][SerializeField] private float _maxDelayClouds;
    [Header("Мин/Макс размер облак")]
    [Range(MinScaleClouds, MaxScaleClouds)][SerializeField] private float _minScaleClouds;
    [Range(MinScaleClouds, MaxScaleClouds)][SerializeField] private float _maxScaleClouds;

    private int _countActiveClouds;

    private void OnEnable()
    {
        for (int i = 0; i < _clouds.Count; i++)
        {
            _clouds[i].EndedMove += OnEndedMove;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _clouds.Count; i++)
        {
            _clouds[i].EndedMove -= OnEndedMove;
        }
    }

    public void StartMoveClouds()
    {
        for (int i = 0; i < _clouds.Count; i++)
        {
            _clouds[i].Activate(_endPositionX);
        }

        GenerateNewClouds();
    }

    private void GenerateNewClouds()
    {
        _countActiveClouds = Random.Range(_minCountClouds, _maxCountClouds);
        Debug.Log(_minCountClouds);
        float durationMoveClouds = Random.Range(_minDurationMoveClouds, _maxDurationMoveClouds);

        for (int i = 0; i < _countActiveClouds; i++)
        {
            Sprite sprite = _spritesClouds[Random.Range(0, _spritesClouds.Count)];
            float delayMove = Random.Range(_minDelayClouds, _maxDelayClouds);
            float size = Random.Range(_minScaleClouds, _maxScaleClouds);
            float positionY = Random.Range(_minYPositionClouds, _maxYPositionClouds);

            _clouds[i].Generate(sprite, durationMoveClouds, delayMove, size, positionY);
        }
    }

    private void OnEndedMove()
    {
        _countActiveClouds--;

        if (_countActiveClouds == 0)
            GenerateNewClouds();
    }
}
