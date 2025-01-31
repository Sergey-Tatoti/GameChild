using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardScale : MonoBehaviour
{
    [SerializeField] private Image _imageReward;

    private int _maxValue = 1;

    public void ChangeScale(int experience, float _durationChangeScale)
    {
        bool isMaxFilled = false;
        float value = experience * 0.01f;

        if (value >= _maxValue)
        {
            value = _maxValue;
            isMaxFilled = true;
        }

        _imageReward.DOFillAmount(value, _durationChangeScale);
    }

    public void SetScale(int experience) => _imageReward.fillAmount = experience * 0.01f;
}