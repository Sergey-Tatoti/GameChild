using GamePush;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private IslandController _islandController;
    [SerializeField] private RewardCompleteLevels _rewardCompleteLevels;
    [SerializeField] private MenuManagerUI _menuManagerUI;
    [SerializeField] private ADScontroller _adsController;

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
        _adsController.ClickedBuyAds += OnClickedBuyAds;
        GP_Payments.OnFetchProducts += OnFetchProducts;
    }

    private void OnDisable()
    {
        _menuManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _menuManagerUI.ClickedButtonSounds -= OnClickedButtonSounds;
        _islandController.ClickedIsland -= OnClickedIsland;
        _adsController.ClickedBuyAds -= OnClickedBuyAds;
        GP_Payments.OnFetchProducts -= OnFetchProducts;
    }

    public void SetBaseValues(Player player, SoundManager soundManager, SaveGame saveGame)
    {
        _soundManager = soundManager;
        _saveGame = saveGame;
        _player = player;

        _menuManagerUI.SetBaseValues(soundManager);
        _adsController.SetBaseValues(soundManager);
        GP_Payments.Fetch();
    }

    public void SetLoadingValues(List<Level> levels, Level newLevel, bool isCompleteLevels)
    {
        _currentNumberLevel = newLevel.Number;
        _islandController.RenderAllIslands(levels, newLevel);
        _adsController.SetLoadingValues(MainManager.IsBuyedAds);

        if (isCompleteLevels)
            _rewardCompleteLevels.OpenFinishReward();
    }

    public void ShowCrossRoad(Level currentLevel, Level newLevel)
    {
        _currentNumberLevel = currentLevel.Number;

        _menuManagerUI.ShowPanelCrossRoad(true);
        _islandController.CompletedLevel(currentLevel, newLevel);
    }

    public void ShowRewardFinishLevels()
    {
        _rewardCompleteLevels.ActivateRewardPanel();
    }

    private void OnFetchProducts(List<FetchProducts> products)
    {
        if (products != null && products.Count > 0)
            _adsController.SetPrice(products[0].price.ToString());
    }

    private void OnClickedButtonPlay()
    {
        ClickedButtonPlayGame?.Invoke(_currentNumberLevel);
    }

    private void OnClickedIsland(int currentNumberLevel)
    {
        _currentNumberLevel = currentNumberLevel;
        OnClickedButtonPlay();
    }

    private void OnClickedBuyAds()
    {
        GP_Payments.Purchase("ADS_OFF", OnPurchaseSuccess, OnPurchaseError);
    }

    public void OnPurchaseSuccess(string productName)
    {
        MainManager.IsBuyedAds = true;
        _saveGame.SaveBuyAds();
        _adsController.SetLoadingValues(true);
        Debug.Log("Buy Ads");
    }

    private void OnPurchaseError() { Debug.Log("Error Buy AdsOFF"); }

    private void OnClickedButtonSounds(bool isOn, bool isMusics)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        _soundManager.TurnOnSounds(isOn, isMusics);
    }
}