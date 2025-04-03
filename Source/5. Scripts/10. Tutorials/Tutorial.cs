using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tutorial : MonoBehaviour
{
    public enum TypeTutorial { Step, Shop}

    [SerializeField] protected TypeTutorial _typeTutorial;
    [SerializeField] protected Level _level;
    [SerializeField] protected float _timeUntilShine;
    [Tooltip("явл€етс€ ли об€зательным дл€ обучени€")][SerializeField] private bool _isSpecial;

    protected ButtonAnimation _buttonAnimationLeft;
    protected ButtonAnimation _buttonAnimationRight;
    protected ButtonAnimation _buttonAnimationUp;
    protected ButtonAnimation _buttonAnimationDown;
    protected ButtonAnimation _buttonAnimationStart;
    protected ButtonAnimation _buttonAnimationBackStep;
    protected ButtonAnimation _buttonAnimationShop;
    protected ButtonAnimation _buttonAnimationBackMenu;
    protected ButtonAnimation _buttonAnimationLamp;
    protected ButtonAnimation _buttonAnimationBackShop;

    protected ButtonAnimation _buttonAnimationCharacterShop;
    protected ButtonAnimation _buttonAnimationGlassesShop;
    protected ButtonAnimation _buttonAnimationTopShop;
    protected ButtonAnimation _buttonAnimationRamkaShop;
    protected ButtonAnimation _buttonAnimationGroundShop;
    protected ButtonAnimation _buttonAnimationCapShop;

    protected List<Button> _allButtons;
    protected List<ShopCardView> _shopCardViews;

    protected bool _isActivate;
    private int _currentLevel;

    #region ----- SetValues -----

    public void SetButtonsGame(Button arrowLeft, Button arrowRight, Button arrowDown, Button arrowUp, Button resetStep,
                         Button startSteps, Button shop, Button backMenu, Button lamp, Button backShop, Level currentLevel)
    {
        _currentLevel = currentLevel.Number;

        _buttonAnimationLeft = arrowLeft.GetComponent<ButtonAnimation>();
        _buttonAnimationDown = arrowDown.GetComponent<ButtonAnimation>();
        _buttonAnimationUp = arrowUp.GetComponent<ButtonAnimation>();
        _buttonAnimationRight = arrowRight.GetComponent<ButtonAnimation>();
        _buttonAnimationStart = startSteps.GetComponent<ButtonAnimation>();
        _buttonAnimationShop = shop.GetComponent<ButtonAnimation>();
        _buttonAnimationBackStep = resetStep.GetComponent<ButtonAnimation>();
        _buttonAnimationBackShop = backShop.GetComponent<ButtonAnimation>();
        _buttonAnimationBackMenu = backMenu.GetComponent<ButtonAnimation>();
        _buttonAnimationLamp = lamp.GetComponent<ButtonAnimation>();

        _allButtons = new List<Button> { arrowLeft, arrowRight, arrowDown, arrowUp, resetStep, startSteps, shop, 
                                         backMenu, lamp, backShop};
    }

    public void SetButtonsShop(Button character, Button glasses, Button top, Button ramka, Button ground, Button cap, 
                               List<ShopCardView> shopCardViews)
    {
        _shopCardViews = shopCardViews;

        _buttonAnimationCharacterShop = character.GetComponent<ButtonAnimation>();
        _buttonAnimationGlassesShop = glasses.GetComponent<ButtonAnimation>();
        _buttonAnimationTopShop = top.GetComponent<ButtonAnimation>();
        _buttonAnimationRamkaShop = ramka.GetComponent<ButtonAnimation>();
        _buttonAnimationGroundShop = ground.GetComponent<ButtonAnimation>();
        _buttonAnimationCapShop = cap.GetComponent<ButtonAnimation>();

        _allButtons.Add(character); _allButtons.Add(glasses); _allButtons.Add(top);
        _allButtons.Add(ramka); _allButtons.Add(ground); _allButtons.Add(cap);
    }

    public void TurnTutorial(bool isActivate)
    {
        if (_currentLevel == _level.Number)
            _isActivate = isActivate;
    }

    #endregion

    #region ----- TutorialSteps -----

    public void TryActivateTutorialSteps(int numberLevel)
    {
        _currentLevel = numberLevel;

        DeactivateTutorialSteps();

        if (_currentLevel == _level.Number && (_isActivate == true || _isSpecial == true) && _typeTutorial == TypeTutorial.Step)
            ActivateTutroialSteps();
        else
            return;
    }

    public void ClickedButtonArrow(Vector3 direction)
    {
        if (_currentLevel == _level.Number && (_isActivate == true || _isSpecial == true) && _typeTutorial == TypeTutorial.Step)
            ShowButtonArrows(direction);
        else
            return;
    }

    public virtual void ActivateTutroialSteps()
    {

    }

    public virtual void DeactivateTutorialSteps()
    {

    }

    public virtual void ShowButtonArrows(Vector3 direction)
    {

    }


    #endregion

    #region ----- TutorialShop -----

    public void TryActivateTutorialShop(int numberLevel, Item item)
    {
        _currentLevel = numberLevel;

        if (numberLevel == _level.Number && _isActivate == false)
        {
            _isActivate = true;
            ActivateTutroialReward(item);
        }
        else
            return;
    }

    public virtual void ActivateTutroialReward(Item item)
    {
        ChangeInterectableButtons(false);
    }

    public virtual void DeactivateTutroialReward()
    {
        ChangeInterectableButtons(true);
    }

    private void ChangeInterectableButtons(bool isInterectable)
    {
        for (int i = 0; i < _allButtons.Count; i++)
        {
            _allButtons[i].interactable = isInterectable;
        }
    }

    #endregion
}