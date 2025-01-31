using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepManagerUI : MonoBehaviour
{
    [SerializeField] private List<Image> _imagesStepWindows = new List<Image>();
    [SerializeField] private Sprite _arrowRight;
    [SerializeField] private Sprite _arrowLeft;
    [SerializeField] private Sprite _arrowUp;
    [SerializeField] private Sprite _arrowDown;

    private List<Vector3> _directions = new List<Vector3>();

    #region ----- TryAddNewStep -----
    public void OnClickedButtonArrow(Vector3 direction)
    {
        if (_directions.Count < _imagesStepWindows.Count)
        {
            _directions.Add(direction);

            ActivateStepWindow(_directions.Count - 1, direction);
        }
    }

    private void ActivateStepWindow(int index, Vector3 direction)
    {
        _imagesStepWindows[index].sprite = GetSpriteByDirection(direction);
        _imagesStepWindows[index].gameObject.SetActive(true);
    }


    #endregion

    #region ----- ResetStep -----
    public void OnClickedButtonResetStep()
    {
        if (_directions.Count > 0)
        {
            int currentIndex = _directions.Count - 1;

            _imagesStepWindows[currentIndex].gameObject.SetActive(false);
            _directions.RemoveAt(currentIndex);
        }
    }

    public void ResetSteps()
    {
        _directions.Clear();

        for (int i = 0; i < _imagesStepWindows.Count; i++) { _imagesStepWindows[i].gameObject.SetActive(false); }
    }
    #endregion

    #region ----- GetValues -----

    public List<Vector3> GetDirections() { return _directions; }

    private Sprite GetSpriteByDirection(Vector3 direction)
    {
        Sprite sprite = null;

        switch(direction)
        {
            case Vector3 v when v.Equals(Vector3.right):
                sprite = _arrowRight;
                break;
            case Vector3 v when v.Equals(Vector3.left):
                sprite = _arrowLeft;
                break;
            case Vector3 v when v.Equals(Vector3.up):
                sprite = _arrowUp;
                break;
            case Vector3 v when v.Equals(Vector3.down):
                sprite = _arrowDown;
                break;
        }

        return sprite;
    }

    #endregion
}