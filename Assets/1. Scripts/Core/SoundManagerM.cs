using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManagerM 
{

    public enum Sound
    {
        PlayerMove,

        PlayerAttackA,
        PlayerAttackB,
        PlayerAttackC,
        EnemyHit,
        EnemyDie,
        ButtonSound,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;


    //private static bool isSound;
    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlahyed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 1f;
                    
                    //아직 플레잉 중이라면 false;

                    if (lastTimePlahyed + playerMoveTimerMax < Time.time)
                    {
                        

                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return  false;
                    }
                }
                else
                {
                    return true;
                }
                //break;
        }

        //return false;
    }


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0;
    }
  public static void PlaySound(Sound sound)
  {
        if(CanPlaySound(sound))
        {
            

            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            audioSource.PlayOneShot(GetAudioClip(sound));
        }

  }

    private static AudioClip GetAudioClip(Sound sound)
    {


        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.SoundAudioClipArray)
        {
            //if (soundAudioClip.audioClip.)
                if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;
    }
}
