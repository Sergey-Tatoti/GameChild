using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private RewardScale _rewardScale;
    [SerializeField] private CardRewardView _cardRewardView;
    [SerializeField] private GameObject _conteinerCardRewardView;
    [SerializeField] private float _durationChangeScale;

    private int _countReward = 3;
    private int _maxExperience = 100;
    private int _experience;
    private List<CardRewardView> cardRewardViews = new List<CardRewardView>();
    private List<Item> _closedItems = new List<Item>();

    public int Experience => _experience;
    public int MaxExperience => _maxExperience;

    public event UnityAction ActivateBoxReward;
    public event UnityAction<Item> ChoisenCardReward;

    public void SetValue(List<Item> items, int experience)
    {
        _experience = experience;
        _rewardScale.SetScale(_experience);

        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].IsOpened)
                _closedItems.Add(items[i]);
        }
    }

    #region ------ FillCardView -----

    public void FillCardViewRewards()
    {
        for (int i = 0; i < _countReward; i++)
        {
            CardRewardView cardRewardView = Instantiate(_cardRewardView, _conteinerCardRewardView.transform);

            cardRewardView.SetValue(_closedItems[Random.Range(0, _closedItems.Count)]);
            cardRewardViews.Add(cardRewardView);

            cardRewardView.ClickedButtonCard += OnClickedButtonCard;
        }

        _conteinerCardRewardView.SetActive(true);
    }

    public void ResetCardViews()
    {
        for (int i = 0; i < cardRewardViews.Count; i++)
        {
            cardRewardViews[i].ClickedButtonCard -= OnClickedButtonCard;

            Destroy(cardRewardViews[i].gameObject);
        }

        cardRewardViews.Clear();
        _conteinerCardRewardView.SetActive(false);
    }

    #endregion

    public void ChangeExperience(int experience) 
    { 
        _experience += experience; 
        _rewardScale.ChangeScale(_experience, _durationChangeScale);

        if(_experience >= _maxExperience)
            ActivateBoxReward?.Invoke();
    }

    private void OnClickedButtonCard(CardRewardView cardRewardView)
    {
        _experience = 0;
        _rewardScale.SetScale(_experience);
        _closedItems.Remove(cardRewardView.Item);

        for (int i = 0; i < cardRewardViews.Count; i++)
        {
            cardRewardViews[i].BlockButton(true);
        }

        ChoisenCardReward?.Invoke(cardRewardView.Item);
    }
}