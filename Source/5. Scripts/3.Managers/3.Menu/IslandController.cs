using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IslandController : MonoBehaviour
{
    [SerializeField] private Avatar _avatar;
    [SerializeField] private List<Island> _islands;

    [SerializeField] private Sprite _spriteBlank;
    [SerializeField] private Sprite _spriteRegular;
    [SerializeField] private Sprite _spriteNextRegular;
    [SerializeField] private Sprite _spriteLockedRegular;

    [SerializeField] private Sprite _spritePortal;
    [SerializeField] private Sprite _spritePortalLocked;

    [SerializeField] private Sprite _spriteSilverStar;

    [SerializeField] private Sprite _spriteKey;
    [SerializeField] private Sprite _spriteKeyLocked;

    [SerializeField] private Sprite _spriteDonate;
    [SerializeField] private Sprite _spriteFinal;

    private List<Level> _levels;
    private Island _currentIsland;

    public event UnityAction<int> ClickedIsland;

    private void OnEnable()
    {
        for (int i = 0; i < _islands.Count; i++)
        {
            _islands[i].ClickedIsland += OnClickedIsland;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _islands.Count; i++)
        {
            _islands[i].ClickedIsland -= OnClickedIsland;
        }
    }

    #region ----- Render Islands -----

    public void RenderAllIslands(List<Level> levels, Level newLevel)
    {
        _levels = levels;

        for (int i = 0; i < levels.Count; i++)
        {
            _islands[i].SetNumber(levels[i].Number);
            RenderIsland(levels[i], newLevel.Number);
        }

        TempDonate(levels.Count);
        TryShowNextIsliand(newLevel);
        SetCurrentIsland(GetIslandByNumber(newLevel.Number));
    }

    private void RenderIsland(Level level, int numberCurrentLevel)
    {
        Sprite sprite = null;
        int number = level.Number;
        bool isUnlock = level.IsCompleted;
        bool isShowNumber = (level.LevelType == LevelInfo.LevelType.Regular || level.LevelType == LevelInfo.LevelType.FisrtBonus) && isUnlock
                            ? true : false;

        switch (level.LevelType)
        {
            case LevelInfo.LevelType.Regular:
                sprite = isUnlock ? _spriteRegular : _spriteLockedRegular;
                GetIslandByNumber(number).Render(sprite, isUnlock, isShowNumber);
                break;
            case LevelInfo.LevelType.FisrtBonus:
                sprite = isUnlock ? _spriteSilverStar : _spriteLockedRegular;
                GetIslandByNumber(number).Render(sprite, isUnlock, isShowNumber);
                break;
            case LevelInfo.LevelType.FirstPortal:
                sprite = isUnlock ? _spritePortal : _spritePortalLocked;
                GetIslandByNumber(number).Render(sprite, isUnlock, isShowNumber);
                break;
            case LevelInfo.LevelType.FisrtKey:
                sprite = isUnlock ? _spriteKey : _spriteKeyLocked;
                GetIslandByNumber(number).Render(sprite, isUnlock, isShowNumber);
                break;
            case LevelInfo.LevelType.Final:
                sprite = isUnlock ? _spriteFinal : _spriteFinal;
                GetIslandByNumber(number).Render(sprite, isUnlock, false);
                break;
        }

        if (level.IsDonate)
            GetIslandByNumber(number).Render(_spriteDonate, false, false);
    }

    private void TempDonate(int countLevel) // временный метод для отображение остальных уровней
    {
        for (int i = countLevel; i < _islands.Count; i++)
        {
            _islands[i].SetNumber(i);
            _islands[i].Render(_spriteDonate, false, false);
        }

        _islands[_islands.Count - 1].Render(_spriteFinal, false, false);
    }

    #endregion

    #region ----- Changed Island -----

    public void CompletedLevel(Level currentLevel, Level newLevel)
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            RenderIsland(_levels[i], currentLevel.Number);
        }

        TryShowNextIsliand(newLevel);
        SetCurrentIsland(GetIslandByNumber(currentLevel.Number));
    }

    private void TryShowNextIsliand(Level newLevel)
    {
        switch (newLevel.LevelType)
        {
            case LevelInfo.LevelType.Regular:
                    GetIslandByNumber(newLevel.Number).Render(_spriteNextRegular, true, true);
                break;
            case LevelInfo.LevelType.FisrtBonus:
                GetIslandByNumber(newLevel.Number).Render(_spriteSilverStar, true, true);
                break;
            case LevelInfo.LevelType.FirstPortal:
                GetIslandByNumber(newLevel.Number).Render(_spritePortal, true, false);
                break;
            case LevelInfo.LevelType.FisrtKey:
                GetIslandByNumber(newLevel.Number).Render(_spriteKey, true, false);
                break;
        }
    }

    private void SetCurrentIsland(Island island)
    {
        _currentIsland = island;
        _currentIsland.Render(_spriteBlank, true, false);
        _avatar.transform.position = island.transform.position;
    }

    private void OnClickedIsland(Island island)
    {
        SetCurrentIsland(island);
        ClickedIsland?.Invoke(_currentIsland.Number);
    }

    #endregion

    #region ----- Get Values -----

    private Island GetIslandByNumber(int number)
    {
        for (int i = 0; i < _islands.Count; i++)
        {
            if (_islands[i].Number == number)
                return _islands[i];
        }
        return null;
    }

    #endregion
}