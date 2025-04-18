using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private GamePlayManager _gamePlayManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SaveGame _saveGame;
    [SerializeField] private Player _player;
    [Space]
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelGame;

    private void OnEnable()
    {
        _menuManager.ClickedButtonPlayGame += OnClickedPlayGame;
        _gamePlayManager.OnClickedBackMenu += OnClickedBackMenu;
    }

    private void OnDisable()
    {
        _menuManager.ClickedButtonPlayGame -= OnClickedPlayGame;
        _gamePlayManager.OnClickedBackMenu -= OnClickedBackMenu;
    }

    private void Start()
    {
        _gamePlayManager.SetBaseValues(_player, _soundManager, _saveGame);
        _menuManager.SetBaseValues(_player, _soundManager, _saveGame);
        _saveGame.LoadAll(this);

        SwitchPanels(true);
    }

    public void SetLoadingValues(int experience, List<int> numbersCompleteLevels, int numberNewLevel, List<int> openedIdItems,
                                 List<int> selectedIdItems, List<int> showedIdItems, int indexGroundAvatar)
    {
        _gamePlayManager.SetLoadingValues(experience, numbersCompleteLevels, numberNewLevel, openedIdItems, selectedIdItems, showedIdItems, indexGroundAvatar);
        _menuManager.SetLoadingValues(_gamePlayManager.Levels, _gamePlayManager.NewLevel);
    }

    private void OnClickedBackMenu()
    {
        _menuManager.ShowCrossRoad(_gamePlayManager.CurrentLevel, _gamePlayManager.NewLevel);
        SwitchPanels(true);
    }

    private void OnClickedPlayGame(int numberLevel)
    {
        _gamePlayManager.PlayGame(numberLevel);
        _soundManager.PlaySound(SoundManager.TypeSound.GameMusic);

        SwitchPanels(false);
    }

    private void SwitchPanels(bool isShowMenu)
    {
        _panelMenu.SetActive(isShowMenu);
        _panelGame.SetActive(!isShowMenu);
    }
}