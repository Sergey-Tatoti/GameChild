using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapLine : MonoBehaviour
{
    [SerializeField] private int _orderInLayer;
    [SerializeField] private List<TilemapRenderer> _tilemapRenderers;

    public int OrderInLayer => _orderInLayer;

    private void Start()
    {
        for (int i = 0; i < _tilemapRenderers.Count; i++)
        {
            _tilemapRenderers[i].sortingOrder += Player.MultiplyOrderInLayer * _orderInLayer;
        }
    }
}
