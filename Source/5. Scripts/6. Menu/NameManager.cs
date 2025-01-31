using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _buttonChangeName;
    [SerializeField] private int _maxCountSymbols;

    public event UnityAction<string> ChangedName;

    private void OnEnable()
    {
        _buttonChangeName.onClick.AddListener(() => _inputField.Select());
        _inputField.onEndEdit.AddListener(ChangeName);
    }

    private void OnDisable()
    {
        _buttonChangeName.onClick.RemoveListener(() => _inputField.Select());
        _inputField.onEndEdit.RemoveListener(ChangeName);
    }

    public void SetValue() => _inputField.characterLimit = _maxCountSymbols;

    private void ChangeName(string name)
    {
        if (name.Length == 0)
            _inputField.text = "NoName";

        ChangedName?.Invoke(_inputField.text);
    }
}