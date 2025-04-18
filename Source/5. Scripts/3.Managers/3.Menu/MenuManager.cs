using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private IslandController _islandController;
    [SerializeField] private MenuManagerUI _menuManagerUI;

    private Player _player;
    private SoundManager _soundManager;
    private SaveGame _saveGame;

    private int _currentNumberLevel;

    public event UnityAction<int> ClickedButtonPlayGame;

    private void OnEnable()
    {
        _menuManagerUI.ClickedButtonPlay += OnClickedButtonPlay;
        _menuManagerUI.ClickedButtonSounds += OnClickedButtonSounds;
        _islandController.ClickedIsland += OnClickedIsland;
    }

    private void OnDisable()
    {
        _menuManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _menuManagerUI.ClickedButtonSounds -= OnClickedButtonSounds;
        _islandController.ClickedIsland -= OnClickedIsland;
    }

    public void SetBaseValues(Player player, SoundManager soundManager, SaveGame saveGame)
    {
        _soundManager = soundManager;
        _saveGame = saveGame;
        _player = player;
    }

    public void SetLoadingValues(List<Level> levels, Level newLevel)
    {
        _currentNumberLevel = newLevel.Number;
        _islandController.RenderAllIslands(levels, newLevel);
    }

    public void ShowCrossRoad(Level currentLevel, Level newLevel)
    {
        _currentNumberLevel = currentLevel.Number;

        _menuManagerUI.ShowPanelCrossRoad(true);
        _islandController.CompletedLevel(currentLevel, newLevel);
    }

    private void OnClickedButtonPlay()
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        ClickedButtonPlayGame?.Invoke(_currentNumberLevel);
    }

    private void OnClickedIsland(int currentNumberLevel)
    {
        _currentNumberLevel = currentNumberLevel;
        OnClickedButtonPlay();
    }

    private void OnClickedButtonSounds(bool isOn, bool isMusics)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        _soundManager.TurnOnSounds(isOn, isMusics);
    }
}