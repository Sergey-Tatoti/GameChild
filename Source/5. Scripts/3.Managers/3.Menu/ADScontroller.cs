using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADScontroller : MonoBehaviour
{
    [SerializeField] private Button _adsButtonMenu;
    [SerializeField] private Button _adsButtonMap;
    [Header("Меню с кнопкой покупки")]
    [SerializeField] private GameObject _adsBuyPanel;
    [SerializeField] private Button _adsBuyButton;
    [SerializeField] private Button _adsReturnButton;
    [Header("Меню с проверкой")]
    [SerializeField] private GameObject _adsCheckPanel;
    [SerializeField] private Button _adsCheckButtonBuy;
    [SerializeField] private Button _adsCheckButtonReturn;
}
