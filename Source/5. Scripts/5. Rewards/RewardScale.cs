using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardScale : MonoBehaviour
{
    [SerializeField] private Image _imageReward;

    public void ChangeScale(int experience, float _durationChangeScale)
    {
        float value = Mathf.Clamp01(experience * 0.01f);

        _imageReward.DOFillAmount(value, _durationChangeScale);
    }

    public void SetScale(int experience) => _imageReward.fillAmount = experience * 0.01f;
}