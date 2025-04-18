using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroundAvatar : MonoBehaviour
{
    public enum TypeAvatarGround { Yellow, Green, Blue, Red }

    [SerializeField] private TypeAvatarGround _typeGround;
    [SerializeField] private Button _buttonGround;
    [SerializeField] private GameObject _selectOutline;
    [SerializeField] private Sprite _spriteRamka;
    [SerializeField] private Sprite _spriteGround;

    public TypeAvatarGround TypeGround => _typeGround;
    public Sprite SpriteRamka => _spriteRamka;
    public Sprite SpriteGround => _spriteGround;

    public event UnityAction<TypeAvatarGround> ClickedButtonGround;

    private void OnEnable()
    {
        _buttonGround.onClick.AddListener(() => ClickedButtonGround?.Invoke(_typeGround));
    }

    private void OnDisable()
    {
        _buttonGround.onClick.RemoveListener(() => ClickedButtonGround?.Invoke(_typeGround));
    }

    public void ShowGround(bool isShow) => _selectOutline.SetActive(isShow);
}