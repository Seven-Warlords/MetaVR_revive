using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("#AudioList")]
    //manage audio source
    public List<AudioSource> BGMAudioList;
    public List<AudioSource> SFXAudioList;

    [Header("#AudioClip")]
    //array audio clip
    public AudioClip[] BGMClip;
    public AudioClip[] SFXClip;

    [Header("#AudioVolume")]
    public float bgmVolume;
    public float sfxVolume;


    //create BGM audio source in obj
    public AudioSource CreateBGMAudioSource(GameObject obj,AudioClip audioClip)
    {
        int check = 0;
        for (int i = 0; i < BGMClip.Length; i++)
        {
            if (audioClip == BGMClip[i])
            {
                break;
            }
            else
            {
                check++;
            }
        }
        if (check == BGMClip.Length)
        {
            return null;
        }

        for (int i = 0; i < BGMAudioList.Count; i++)
        {
            if (BGMAudioList[i].gameObject == obj)
            {
                if (BGMAudioList[i].clip == audioClip)
                {
                    BGMAudioList[i].volume = sfxVolume;

                    //오디오 플레이
                    BGMAudioList[i].Play();
                    return BGMAudioList[i];
                }
            }
        }

        AudioSource source;
        //오디오 생성
        source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false; //바로 작동되는거 차단.한번만 출력X
        //거리별 소리 생성
        source.spatialBlend = 1f;
        //오디오 등록
        source.clip = audioClip;
        //오디오 반복
        source.loop = true;
        //오디오 플레이
        source.Play();

        //BGM 저장
        BGMAudioList.Add(source);
        return source;
    }
    //create SFX audio source in obj
    public AudioSource CreateSFXAudioSource(GameObject obj,AudioClip audioClip)
    {
        int check = 0;
        for(int i = 0; i < SFXClip.Length; i++)
        {
            if (audioClip == SFXClip[i])
            {
                break;
            }
            else
            {
                check++;
            }
        }
        if (check == SFXClip.Length){
            return null;
        }
        for (int i = 0; i < SFXAudioList.Count; i++)
        {
            if (SFXAudioList[i].gameObject==obj)
            {
                if (SFXAudioList[i].clip == audioClip)
                {
                    SFXAudioList[i].volume = sfxVolume;

                    //오디오 플레이
                    SFXAudioList[i].Play();
                    return SFXAudioList[i];
                }
            }
        }

        AudioSource source;
        //오디오 생성
        source =obj.AddComponent<AudioSource>();
        source.playOnAwake = false; //바로 작동되는거 차단.한번만 출력X
        //오디오 등록
        source.clip = audioClip;
        //오디오 플레이
        source.Play();

        //SFX 저장
        SFXAudioList.Add(source);
        return source;
    }
    public int StopAudioSource(GameObject obj, AudioClip audioClip)
    {
        AudioSource source;
        if (obj.TryGetComponent<AudioSource>(out source))
        {
            if (source.clip = audioClip)
            {
                //오디오 플레이
                source.Stop();
                return 1;
            }

        }
        return 0;
    }
    public AudioClip FindBGMAudioClipByString(string str)
    {
        for (int i = 0; i < BGMClip.Length; i++)
        {
            if (BGMClip[i].name == str)
            {
                return BGMClip[i];
            }
        }
        return null;
    }
    public AudioClip FindBGMAudioClipByInt(int num)
    {
        for (int i = 0; i < BGMClip.Length; i++)
        {
            if (i == num)
            {
                return BGMClip[i];
            }
        }
        return null;
    }
    public AudioClip FindSFXAudioClipByString(string str)
    {
        for (int i = 0; i < SFXClip.Length; i++)
        {
            if (SFXClip[i].name == str)
            {
                return SFXClip[i];
            }
        }
        return null;
    }
    public AudioClip FindSFXAudioClipByInt(int num)
    {
        for (int i = 0; i < SFXClip.Length; i++)
        {
            if (i == num)
            {
                return SFXClip[i];
            }
        }
        return null;
    }

    public void Init()
    {
        //볼륨 초기화
        if (GameManager.instance.Load("BGM") == -1)
        {
            bgmVolume = 0.5f;
        }
        else
        {
            bgmVolume = GameManager.instance.Load("BGM");
        }
        if (GameManager.instance.Load("SFX") == -1)
        {
            sfxVolume = 0.5f;
        }
        else
        {
            sfxVolume = GameManager.instance.Load("SFX");
        }

        //배경음 플레이어 초기화
        foreach(AudioSource audio in BGMAudioList)
        {
            audio.playOnAwake = false; //바로 작동되는거 차단.한번만 출력X
            audio.loop = true; //반복
            audio.volume = bgmVolume;
        }

        //효과음 플레이어 초기화
        foreach (AudioSource audio in SFXAudioList)
        {
            audio.playOnAwake = false; //바로 작동되는거 차단.한번만 출력X
            audio.volume = sfxVolume;
        }
        return;
    }
}
