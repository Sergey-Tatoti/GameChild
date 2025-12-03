using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdScale : MonoBehaviour
{
    [SerializeField] private Image _imageScaleBackGroundAd;
    [SerializeField] private Image _imageScaleAd;

    public void ActivateScale(bool isActivate, float timeActivate)
    {
        _imageScaleAd.fillAmount = isActivate ? 1 : 0;
        _imageScaleBackGroundAd.gameObject.SetActive(isActivate);

        _imageScaleAd.DOFillAmount(0, timeActivate);
    }
}