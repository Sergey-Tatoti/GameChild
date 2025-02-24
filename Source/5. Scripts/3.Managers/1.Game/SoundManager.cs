using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum TypeSound { ClickButton, ClickButtonPlay, ClickItem, OpenRewardBox, MoveRewardBox, SwitchLevel, TouchedStar, 
                         TouchedHitBox, TouchedMiniStar, TouchedTeleport, TouchedKey, TouchedCloseLock, UseStep, OpenDoor, GameMusic}

    [SerializeField] private AudioSource _musicGame;
    [Header("Кнопки")]
    [SerializeField] private AudioSource _soundClickButton;
    [SerializeField] private AudioSource _soundClickButtonPlay;
    [SerializeField] private AudioSource _soundClickItem;
    [Header("Подарок")]
    [SerializeField] private AudioSource _soundOpenRewardBox;
    [SerializeField] private AudioSource _soundMoveRewardBox;
    [SerializeField] private AudioSource _soundSwitchLevel;
    [Header("Касания элементов")]
    [SerializeField] private AudioSource _soundTouchedStar;
    [SerializeField] private AudioSource _soundTouchedMiniStar;
    [SerializeField] private AudioSource _soundTouchedHitBox;
    [SerializeField] private AudioSource _soundTouchedTeleport;
    [SerializeField] private AudioSource _soundTouchedKey;
    [SerializeField] private AudioSource _soundTouchedCloseDoor;
    [Header("Активные действия игрока")]
    [SerializeField] private AudioSource _soundUseStep;
    [SerializeField] private AudioSource _soundOpenDoor;

    [Header("Все звуки и музыка")]
    [SerializeField] private List<AudioSource> _musics;
    [SerializeField] private List<AudioSource> _soundsEffects;

    public void TurnOnSounds(bool isOn, bool isMusic)
    {
        if (isMusic)
            TurnOn(isOn, _musics);
        else
            TurnOn(isOn, _soundsEffects);
    }

    public void PlaySound(TypeSound typeSound)
    {
        switch (typeSound)
        {
            case TypeSound.ClickButton:
                _soundClickButton.Play();
                break;
            case TypeSound.ClickButtonPlay:
                _soundClickButtonPlay.Play();
                break;
            case TypeSound.ClickItem:
                _soundClickItem.Play();
                break;
            case TypeSound.OpenRewardBox:
                _soundOpenRewardBox.Play();
                break;
            case TypeSound.MoveRewardBox:
                _soundMoveRewardBox.Play();
                break;
            case TypeSound.SwitchLevel:
                _soundSwitchLevel.Play();
                break;
            case TypeSound.TouchedStar:
                _soundTouchedStar.Play();
                break;
            case TypeSound.TouchedHitBox:
                _soundTouchedHitBox.Play();
                break;
            case TypeSound.TouchedTeleport:
                _soundTouchedTeleport.Play();
                break;
            case TypeSound.TouchedMiniStar:
                _soundTouchedMiniStar.Play();
                break;
            case TypeSound.TouchedKey:
                _soundTouchedKey.Play();
                break;
            case TypeSound.TouchedCloseLock:
                _soundTouchedCloseDoor.Play();
                break;
            case TypeSound.UseStep:
                _soundUseStep.Play();
                break;
            case TypeSound.OpenDoor:
                _soundOpenDoor.Play();
                break;
            case TypeSound.GameMusic:
                _musicGame.Play();
                break;
        }
    }

    private void TurnOn(bool isOn, List<AudioSource> sounds)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].gameObject.SetActive(isOn);
        }
    }
}