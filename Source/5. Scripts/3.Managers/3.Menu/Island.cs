using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Vector3 _spawnPosition;

    public Vector3 SpawnPosition => _spawnPosition;

    public void Render(Sprite sprite, int levelNumber)
    {
        _image.sprite = sprite;
        _levelNumber.text = levelNumber.ToString();
    }
}