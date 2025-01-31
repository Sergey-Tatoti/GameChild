using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _winButtonNextLevel;
    [SerializeField] private Button _winButtonMainMenu;

    public event UnityAction ClickedButtonNewLevel;
    public event UnityAction ClickedButtonMainMenu;

    private void OnEnable()
    {
        _winButtonNextLevel.onClick.AddListener(OnClickedButtonNewLevel);
        _winButtonMainMenu.onClick.AddListener(() => ClickedButtonMainMenu?.Invoke());
    }

    private void OnDisable()
    {
        _winButtonNextLevel.onClick.RemoveListener(OnClickedButtonNewLevel);
        _winButtonMainMenu.onClick.RemoveListener(() => ClickedButtonMainMenu?.Invoke());
    }

    public void ShowWinPanel() => _winPanel.SetActive(true);

    private void OnClickedButtonNewLevel()
    {
        _winPanel.gameObject.SetActive(false);
        ClickedButtonNewLevel?.Invoke();
    }
}