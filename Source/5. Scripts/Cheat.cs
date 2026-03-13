using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cheat : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private float _timerGetOffAds = 5;
    private MenuManager _menuManager;
    private Coroutine _coroutine;

    private void Start()
    {
        _menuManager = FindFirstObjectByType<MenuManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("work");
        _coroutine = StartCoroutine(TimerGetOffAds());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator TimerGetOffAds()
    {
        yield return new WaitForSeconds(_timerGetOffAds);

        _menuManager.OnPurchaseSuccess("ADS_OFF");
    }
}
