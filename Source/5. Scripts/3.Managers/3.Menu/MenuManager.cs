using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private MenuManagerUI _menuManagerUI;

    private Player _player;
    private SoundManager _soundManager;
    private SaveGame _saveGame;

    private int _maxNumberlevel;

    public event UnityAction<int> ClickedButtonPlayGame;

    private void OnEnable()
    {
        _menuManagerUI.ClickedButtonPlay += OnClickedButtonPlay;
        _menuManagerUI.ClickedButtonSounds += OnClickedButtonSounds;
    }

    private void OnDisable()
    {
        _menuManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _menuManagerUI.ClickedButtonSounds -= OnClickedButtonSounds;
    }

    public void SetBaseValues(Player player, SoundManager soundManager, SaveGame saveGame)
    {
        _soundManager = soundManager;
        _saveGame = saveGame;
        _player = player;
    }

    public void SetLoadingValues(int maxNumberlevel)
    {   
        _maxNumberlevel = maxNumberlevel;
    }

    private void OnClickedButtonPlay()
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        ClickedButtonPlayGame?.Invoke(_maxNumberlevel);
    }

    private void OnClickedButtonSounds(bool isOn, bool isMusics)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        _soundManager.TurnOnSounds(isOn, isMusics);
    }
}