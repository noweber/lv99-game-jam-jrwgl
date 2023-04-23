using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXTYPE
{
    player_attack = 0,
    health_reduction,
    yell,
    breath
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource as_sfx;
    private AudioSource as_music;
    //private AudioSource as_music_2;
    [SerializeField] private SFXGroup[] sfxGroups;

    [System.Serializable]
    private class SFXGroup
    {
        [SerializeField] private string name;
        public AudioClip[] sounds;
        public float[] volumes;
    }

    void Awake()
    {
        instance = this;
        as_sfx = this.transform.GetChild(0).GetComponent<AudioSource>();
        as_music = this.transform.GetChild(1).GetComponent<AudioSource>();
        //as_music_2 = transform.Find("AS_Music_2").GetComponent<AudioSource>();
    }

    void Update()
    {
        /*if (PauseMenu.GameIsPaused)
        {
            as_music_1.Pause();
            as_music_2.Pause();
            as_sfx.Pause();
        }
        else
        {
            as_music_1.UnPause();
            as_music_2.UnPause();
            as_sfx.UnPause();
        }*/

    }

    public void RequestSFX(SFXTYPE sfx)
    {
        as_sfx.PlayOneShot(sfxGroups[(int)sfx].sounds[Random.Range(0, sfxGroups[(int)sfx].sounds.Length)],
            Random.Range(sfxGroups[(int)sfx].volumes[0], sfxGroups[(int)sfx].volumes[1]));
    }
}