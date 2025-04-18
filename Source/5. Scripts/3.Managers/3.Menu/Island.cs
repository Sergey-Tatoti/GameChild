using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Vector3 _spawnPosition;

    private int _number;

    public Vector3 SpawnPosition => _spawnPosition;
    public int Number => _number;

    public event UnityAction<Island> ClickedIsland;

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickedButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickedButton);
    }

    private void ClickedButton() => ClickedIsland?.Invoke(this);

    public void SetNumber(int levelNumber)
    {
        _number = levelNumber;
        _levelNumber.text = levelNumber.ToString();
    }

    public void Render(Sprite sprite, bool isUnlock, bool isShowNumber)
    {
        _image.sprite = sprite;
        _levelNumber.gameObject.SetActive(isShowNumber);
        _button.interactable = isUnlock;
    }
}