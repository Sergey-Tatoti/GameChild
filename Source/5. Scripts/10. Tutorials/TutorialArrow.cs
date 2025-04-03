using UnityEngine; //

public class TutorialArrow : MonoBehaviour
{
    [SerializeField] private Sprite _spriteLeftArrow;
    [SerializeField] private Sprite _spriteRightArrow;
    [SerializeField] private Sprite _spriteUpArrow;
    [SerializeField] private Sprite _spriteDownArrow;

    public void ShowArrow(bool isShow, Vector2 direction)
    {
        GetComponent<SpriteRenderer>().sprite = GetSpriteByDirection(direction);
        gameObject.SetActive(isShow);
    }

    private Sprite GetSpriteByDirection(Vector2 direction)
    {
        if(direction == Vector2.left)
            return _spriteLeftArrow;
        else if(direction == Vector2.right)
            return _spriteRightArrow;
        else if (direction == Vector2.up)
            return _spriteUpArrow;
        else 
            return _spriteDownArrow;
    }
}