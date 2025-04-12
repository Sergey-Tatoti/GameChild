using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    [SerializeField] private List<Island> _islands;

    [SerializeField] private Sprite _spriteBlank;
    [SerializeField] private Sprite _spritePortal;
    [SerializeField] private Sprite _spritePortalLocked;
    [SerializeField] private Sprite _spriteSilverStar;
    [SerializeField] private Sprite _spriteRegular;
    [SerializeField] private Sprite _spriteLockedStar;
    [SerializeField] private Sprite _spriteNextStar;
    [SerializeField] private Sprite _spriteCup;
    [SerializeField] private Sprite _spriteKey;
    [SerializeField] private Sprite _spriteKeyLocked;
    [SerializeField] private Sprite _spriteDonate;

    private Island _currentIsland;

    public void RenderIslands(List<Level> levels)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            switch(levels[i].LevelType)
            {
                case LevelInfo.LevelType.Regular:
                    _islands[i].Render(_spriteRegular, levels[i].Number);
                    break;
                case LevelInfo.LevelType.FisrtBonus:
                    break;
                case LevelInfo.LevelType.FirstPortal:
                    break;
                case LevelInfo.LevelType.FisrtKey:
                    break;
            }
        }
    }

}
