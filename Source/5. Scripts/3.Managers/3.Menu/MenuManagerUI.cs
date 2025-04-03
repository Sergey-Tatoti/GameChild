using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelCrossRoad;
    [Space]
    [SerializeField] private Button _buttonSounds;
    [SerializeField] private Button _buttonMusics;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonCrossRoad;
    [SerializeField] private Button _buttonBackMenu;
    [Space]
    [SerializeField] private Sprite _spriteButtonOnSounds;
    [SerializeField] private Sprite _spriteButtonOffSounds;
    [SerializeField] private Sprite _spriteButtonOnMusics;
    [SerializeField] private Sprite _spriteButtonOffMusics;

    private bool _isOnSounds = true;
    private bool _isOnMusics = true;
    private bool _isMusics = true;

    public event UnityAction ClickedButtonPlay;
    public event UnityAction<bool, bool> ClickedButtonSounds;

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(ClickedButtonPlayGame);
        _buttonCrossRoad.onClick.AddListener(() => ShowPanelCrossRoad(true));
        _buttonBackMenu.onClick.AddListener(() => ShowPanelCrossRoad(false));
        _buttonSounds.onClick.AddListener(ClickedButtonSound);
        _buttonMusics.onClick.AddListener(ClickedButtonMusic);
    }

    private void OnDisable()
    {
        _buttonPlay.onClick.RemoveListener(ClickedButtonPlayGame);
        _buttonCrossRoad.onClick.RemoveListener(() => ShowPanelCrossRoad(true));
        _buttonBackMenu.onClick.RemoveListener(() => ShowPanelCrossRoad(false));
        _buttonSounds.onClick.RemoveListener(ClickedButtonSound);
        _buttonMusics.onClick.RemoveListener(ClickedButtonMusic);
    }

    private void ShowPanelCrossRoad(bool isShow) => _panelCrossRoad.SetActive(isShow);

    private void ClickedButtonPlayGame()
    {
        ClickedButtonPlay?.Invoke();
        ShowPanelCrossRoad(true);
    }

    private void ClickedButtonSound()
    {
        _isOnSounds = !_isOnSounds;
        _buttonSounds.image.sprite = _isOnSounds ? _spriteButtonOnSounds : _spriteButtonOffSounds;

        ClickedButtonSounds?.Invoke(_isOnSounds, !_isMusics);
    }

    private void ClickedButtonMusic()
    {
        _isOnMusics = !_isOnMusics;
        _buttonMusics.image.sprite = _isOnMusics ? _spriteButtonOnMusics : _spriteButtonOffMusics;

        ClickedButtonSounds?.Invoke(_isOnMusics, _isMusics);
    }
}