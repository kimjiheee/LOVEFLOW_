using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : DontDestroy<SoundManager>
{
    public enum AudioType
    {
        BGM,
        Effect,
        Max
    }

    public enum ClipBGM
    {
        BGM_Opening,
        BGM_Stage
    }

    public enum ClipEffect
    {
        CH_Jump,
        CH_Life,
        CH_Walk,
        Control_Node,
        Fluid_On_Floor,
        Get_Hammer,
        Input_A,
        Input_Move,
        Input_X,
        Install_mode_Btn_B,
        Install_mode_Pipe_Turn,
        Pipe_Destory,
        Pipe_Fix,
        Pip_Fluid_Check,
        Pipe_Siren,
        Stage_Talk,
        Stage_UI_Clear,
        Stage_UI_Fail,
        Stage_UI_Start,
        Max
    }

    //[SerializeField]
    public AudioClip[] _bgmClips;
    //[SerializeField]
    public AudioClip[] _effectClips;
    public AudioSource[] _audio;
    int playMaxCount = 5;
    Dictionary<ClipEffect, int> _effectPlayList = new Dictionary<ClipEffect, int>();


    IEnumerator Coroutine_CheckPlayEffect(ClipEffect effect, float time)
    {
        yield return new WaitForSeconds(time);
        int count = 0;

        _effectPlayList.TryGetValue(effect, out count);
        if (count == 1)
        {
            _effectPlayList.Remove(effect);
        }
        else
        {
            _effectPlayList[effect]--;
        }
    }

    public void PlayBGM(ClipBGM bgm)
    {
        _audio[(int)AudioType.BGM].clip = _bgmClips[(int)bgm];
        _audio[(int)AudioType.BGM].loop = true;
        _audio[(int)AudioType.BGM].Play();
    }

    public void StopBGM(ClipBGM bgm)
    {
        _audio[(int)AudioType.BGM].clip = _bgmClips[(int)bgm];
        _audio[(int)AudioType.BGM].Stop();
    }

    public void PlayEffect(ClipEffect effect)
    {
        int count = 0;

        _effectPlayList.TryGetValue(effect, out count);
        if (count < playMaxCount)
        {
            _audio[(int)AudioType.Effect].PlayOneShot(_effectClips[(int)effect]);
            StartCoroutine(Coroutine_CheckPlayEffect(effect, _effectClips[(int)effect].length));
            if (_effectPlayList.ContainsKey(effect))
            {
                _effectPlayList[effect]++;
            }
            else
            {
                _effectPlayList.Add(effect, 1);
            }
        }
    }

    protected override void OnAwake()
    {
        _audio = new AudioSource[(int)AudioType.Max];

        _audio[(int)AudioType.BGM] = gameObject.AddComponent<AudioSource>();
        _audio[(int)AudioType.BGM].loop = true;
        _audio[(int)AudioType.BGM].playOnAwake = false;
        _audio[(int)AudioType.BGM].rolloffMode = AudioRolloffMode.Linear;

        _audio[(int)AudioType.Effect] = gameObject.AddComponent<AudioSource>();
        _audio[(int)AudioType.Effect].loop = false;
        _audio[(int)AudioType.Effect].playOnAwake = false;
        _audio[(int)AudioType.Effect].rolloffMode = AudioRolloffMode.Linear;

        _bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        _effectClips = Resources.LoadAll<AudioClip>("Sound/Effect");
    }


    /// <summary>
    /// 환경설정 슬라이더에서 가져다 쓸 함수들
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        _audio[(int)AudioType.BGM].volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _audio[(int)AudioType.Effect].volume = volume;
    }

    public float GetBGMVolume()
    {
        return _audio[(int)AudioType.BGM].volume;
    }

    public float GetSFXVolume()
    {
        return _audio[(int)AudioType.Effect].volume;
    }
}
