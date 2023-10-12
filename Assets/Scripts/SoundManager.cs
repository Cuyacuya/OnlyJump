using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] List<AudioClip> playlist = new List<AudioClip>();

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public AudioClip GetClipFromPlaylist(string clip_name)
    {
        for (int i = 0; i < playlist.Count; i++)
        {
            if (clip_name == playlist[i].name)
            {
                return playlist[i];
            }
        }

        Debug.LogWarning(clip_name + " does not exist in the playlist.");
        return null;
    }

    public void PlayEffectSound()
    {

    }
}
