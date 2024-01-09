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

                    //����� �÷���
                    BGMAudioList[i].Play();
                    return BGMAudioList[i];
                }
            }
        }

        AudioSource source;
        //����� ����
        source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false; //�ٷ� �۵��Ǵ°� ����.�ѹ��� ���X
        //�Ÿ��� �Ҹ� ����
        source.spatialBlend = 1f;
        //����� ���
        source.clip = audioClip;
        //����� �ݺ�
        source.loop = true;
        //����� �÷���
        source.Play();

        //BGM ����
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

                    //����� �÷���
                    SFXAudioList[i].Play();
                    return SFXAudioList[i];
                }
            }
        }

        AudioSource source;
        //����� ����
        source =obj.AddComponent<AudioSource>();
        source.playOnAwake = false; //�ٷ� �۵��Ǵ°� ����.�ѹ��� ���X
        //����� ���
        source.clip = audioClip;
        //����� �÷���
        source.Play();

        //SFX ����
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
                //����� �÷���
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
        //���� �ʱ�ȭ
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

        //����� �÷��̾� �ʱ�ȭ
        foreach(AudioSource audio in BGMAudioList)
        {
            audio.playOnAwake = false; //�ٷ� �۵��Ǵ°� ����.�ѹ��� ���X
            audio.loop = true; //�ݺ�
            audio.volume = bgmVolume;
        }

        //ȿ���� �÷��̾� �ʱ�ȭ
        foreach (AudioSource audio in SFXAudioList)
        {
            audio.playOnAwake = false; //�ٷ� �۵��Ǵ°� ����.�ѹ��� ���X
            audio.volume = sfxVolume;
        }
        return;
    }
}
