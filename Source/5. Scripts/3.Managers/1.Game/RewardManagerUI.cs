using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardManagerUI : MonoBehaviour
{
    [SerializeField] private RewardScale _rewardScale;
    [SerializeField] private CardRewardView _cardRewardView;
    [SerializeField] private RewardBoxes _rewardBoxes;
    [Space]
    [SerializeField] private GameObject _templateSpawnCard;
    [SerializeField] private List<Transform> _placesCardReward;
    [SerializeField] private Transform _pointSpawnCard;
    [SerializeField] private Transform _pointMoveCardAfterChoose;
    [Space]
    [Tooltip("Продложительность движения шкалы подарка")][SerializeField] private float _durationChangeScale;
    [Tooltip("Продложительность движения карты из подарка")][SerializeField] private float _durationMoveCardBox;
    [Tooltip("Продложительность движения карты в магазин")][SerializeField] private float _durationMoveCardShop;
    [Tooltip("Продолжительность увлеичения подарка от 0 до end")][SerializeField] private float _durationChangeScaleRewardBox;
    [Tooltip("Задержка после выбора карты, чтобы разглядеть приз")][SerializeField] private float _delayShowCard;

    private List<CardRewardView> _cardRewardViews = new List<CardRewardView>();
    private List<Item> _closedItems = new List<Item>();
    private int _countReward;

    public event UnityAction OpenedBigBoxReward;
    public event UnityAction<Item> ChoisenCardReward;

    private void OnEnable()
    {
        _rewardBoxes.ClickedBigBoxReward += OnClickedBigBoxReward;
    }

    private void OnDisable()
    {
        _rewardBoxes.ClickedBigBoxReward -= OnClickedBigBoxReward;
    }

    public void SetValue(List<Item> items, int experience)
    {
        _rewardScale.SetScale(experience);
        _rewardBoxes.SetValue();

        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].IsOpened)
                _closedItems.Add(items[i]);
        }
    }

    public void ChangeExperience(int experience) => _rewardScale.ChangeScale(experience, _durationChangeScale);

    #region ----- Activate Reward -----

    public void ActivateReward(int countReward)
    {
        _countReward = countReward;
        _rewardBoxes.ActivateBoxesRewards(_durationChangeScaleRewardBox);
    }

    private void OnClickedBigBoxReward()
    {
        OpenedBigBoxReward?.Invoke();

        FillCardViewReward();
    }

    private void FillCardViewReward()
    {
        CardRewardView cardRewardView;

        CreateCardView(out cardRewardView);
        MoveCardView(cardRewardView, _placesCardReward[_countReward - 1]);

        _rewardBoxes.PlayAnimationShowCard();
        _countReward--;
    }

    private void CreateCardView(out CardRewardView cardRewardView)
    {
        cardRewardView = Instantiate(_cardRewardView, _pointSpawnCard.transform);
        cardRewardView.SetValue(_closedItems[Random.Range(0, _closedItems.Count)]);
        cardRewardView.BlockButton(true);

        cardRewardView.transform.position = _templateSpawnCard.transform.position;
        cardRewardView.transform.localScale = _templateSpawnCard.transform.localScale;
        _cardRewardViews.Add(cardRewardView);

        cardRewardView.ClickedButtonCard += OnClickedButtonCard;
    }

    private void MoveCardView(CardRewardView cardRewardView, Transform cardRewardPlace)
    {
        cardRewardView.transform.DOScale(1, _durationMoveCardBox);
        cardRewardView.transform.DOMove(cardRewardPlace.transform.position, _durationMoveCardBox).OnComplete(() =>
        {
            cardRewardPlace.transform.SetParent(cardRewardPlace);
            TryTakeNextCardView();
        });
    }

    private void TryTakeNextCardView()
    {
        if (_countReward > 0)
            FillCardViewReward();
        else
            ActivateCardsReward(false);
    }

    private void ActivateCardsReward(bool isActivate)
    {
        for (int i = 0; i < _cardRewardViews.Count; i++)
        {
            _cardRewardViews[i].BlockButton(isActivate);
        }
    }

    #endregion

    #region ----- Reset Rewards -----

    private void OnClickedButtonCard(CardRewardView cardRewardView)
    {
        _rewardScale.SetScale(0);
        _closedItems.Remove(cardRewardView.Item);

        ActivateCardsReward(true);

        StartCoroutine(WaitShowCardView(cardRewardView));
    }

    private void ResetCardViews()
    {
        for (int i = 0; i < _cardRewardViews.Count; i++)
        {
            _cardRewardViews[i].ClickedButtonCard -= OnClickedButtonCard;

            Destroy(_cardRewardViews[i].gameObject);
        }

        _cardRewardViews.Clear();
    }

    private IEnumerator WaitShowCardView(CardRewardView cardRewardView)
    {
        yield return new WaitForSeconds(_delayShowCard);

        cardRewardView.GetComponent<ButtonAnimation>().ResetAnimation();
        cardRewardView.GetComponent<ButtonAnimation>().enabled = false;

        cardRewardView.transform.SetParent(_pointMoveCardAfterChoose);
        cardRewardView.transform.DOMove(_pointMoveCardAfterChoose.position, _durationMoveCardShop);
        cardRewardView.transform.DOScale(0, _durationMoveCardShop).OnComplete(() => ResetCardViews());

        _rewardBoxes.HideRewards(_durationChangeScaleRewardBox);

        ChoisenCardReward?.Invoke(cardRewardView.Item);
    }

    #endregion
}