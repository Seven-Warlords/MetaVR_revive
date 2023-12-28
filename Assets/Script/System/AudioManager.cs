using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : OrderBehaviour
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

    public AudioManager() : base(1){}

    //create BGM audio source in obj
    public AudioSource CreateBGMAudioSource(GameObject obj,AudioClip audioClip)
    {
        AudioSource source;
        if (obj.TryGetComponent<AudioSource>(out source))
        {
            if(source.clip = audioClip)
            {
                source.playOnAwake = false; //�ٷ� �۵��Ǵ°� ����.�ѹ��� ���X
                source.volume = bgmVolume;

                //����� �÷���
                source.Play();
                return source;
            }

        }

        //����� ����
        source = obj.AddComponent<AudioSource>();
        //�Ÿ��� �Ҹ� ����
        source.spatialBlend = 1f;
        //����� ���
        source.clip = audioClip;
        //����� �÷���
        source.Play();

        //BGM ����
        BGMAudioList.Add(source);
        return source;
    }
    //create SFX audio source in obj
    public AudioSource CreateSFXAudioSource(GameObject obj,AudioClip audioClip)
    {
        AudioSource source;
        if (obj.TryGetComponent<AudioSource>(out source))
        {
            if (source.clip = audioClip)
            {
                source.volume = sfxVolume;

                //����� �÷���
                source.Play();
                return source;
            }

        }
        //����� ����
        source=obj.AddComponent<AudioSource>();
        //�Ÿ��� �Ҹ� ����
        source.spatialBlend = 1f;
        //����� ���
        source.clip = audioClip;
        //����� �÷���
        source.Play();

        //SFX ����
        SFXAudioList.Add(source);
        return source;
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

    void Init()
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

    public override void NumberAwake()
    {
        Init();
    }

    public override void NumberStart()
    {
    }
}
