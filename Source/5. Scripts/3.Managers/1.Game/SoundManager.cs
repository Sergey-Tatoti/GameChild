using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum TypeSound { ClickButton, ClickRewardBox, ChangeClothes, WinLevel, HitBox, Step, Teleport, GameMusic }

    [SerializeField] private AudioSource _soundClickButton;
    [SerializeField] private AudioSource _soundClickRewardBox;
    [SerializeField] private AudioSource _soundWinLevel;
    [SerializeField] private AudioSource _soundChangeClothes;
    [SerializeField] private AudioSource _soundHitBox;
    [SerializeField] private AudioSource _soundUseStep;
    [SerializeField] private AudioSource _soundTeleport;
    [SerializeField] private AudioSource _musicGame;
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
            case TypeSound.ClickRewardBox:
                _soundClickRewardBox.Play();
                break;
            case TypeSound.ChangeClothes:
                _soundChangeClothes.Play();
                break;
            case TypeSound.WinLevel:
                _soundWinLevel.Play();
                break;
            case TypeSound.HitBox:
                _soundHitBox.Play();
                break;
            case TypeSound.Step:
                _soundUseStep.Play();
                break;
            case TypeSound.Teleport:
                _soundTeleport.Play();
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