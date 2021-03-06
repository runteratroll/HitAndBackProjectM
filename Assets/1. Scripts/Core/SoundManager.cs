using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Sound {
    public string soundName;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour {
    public static SoundManager instance; //Static 공유 변수. 어디서든 쉽게 참조 변경이 가능함
    [Header("사운드 목록")]
    [SerializeField] Sound[] bgmSounds;

    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;//배열로 만든이유 효과음은 동시에 여러개가 재생될 수 있기 때문!


    public void Start() {
        instance = this; //인스턴스에 자기자신을 넣음 뭔가 c++ int &a 같네 
       PlayRandomBGM();
    }


    //이걸 이제 빠르게 만들어야하는

    public void PlaySE(string _soundName) {
        //와 이거최적화 해야겠는걸? 이진탐색트리로?
        for (int i = 0; i < sfxSounds.Length; i++) {
            if (_soundName == sfxSounds[i].soundName) {//재생중이지 않은 플레이어를 찾아야됨
                for (int x = 0; x < sfxPlayer.Length; x++) {
                    if (!sfxPlayer[x].isPlaying) //x번쨰의 MP3 플레이어가 재생중이지 않다면 만족하는 조건문
                    {
                        //재생중이지 않으면
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].Play();
                        return; //원하는 효과음을 찾았으므로 return;
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다."); //if문에 걸리지 않았으므로 모든 MP3 플레이어는 재생중인 상태
                return;
            }
        }
        Debug.Log("등록된 효과음이 없다");
    }
    public void PlayRandomBGM() {
        int random = Random.Range(0, bgmSounds.Length);
        //rand = sfxSounds.Length
        bgmPlayer.clip = bgmSounds[random].clip;

        bgmPlayer.Play();

    }
    // Start is called before the first frame update


    // Update is called once per frame

}

