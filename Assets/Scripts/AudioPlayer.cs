using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource sourcePrefab;
    private static AudioPlayer instance;

    public static void PlayAudio(AudioClip clip, Vector2 pitch, Vector2 volume)
    {
        instance._PlayAudio(clip, Random.Range(pitch.x, pitch.y), Random.Range(volume.x, volume.y));
    }
    
    public static void PlayAudio(AudioClip clip, float pitch, float volume)
    {
        instance._PlayAudio(clip, pitch, volume);
    }

    private Dictionary<int, List<AudioSource>> _activeSources;
    private Stack<AudioSource> _availableSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        _activeSources = new Dictionary<int, List<AudioSource>>();
        _availableSources = new Stack<AudioSource>();
    }

    public void _PlayAudio(AudioClip clip, float pitch, float volume)
    {
        if (clip == null)
        {
            Debug.LogError("Trying to play a clip that doesn't exist");
        }

        int id = clip.GetInstanceID();
        if (!_activeSources.ContainsKey(id))
        {
            _activeSources.Add(id, new List<AudioSource>());
        }
        else if (_activeSources[id].Count > 10)
        {
            return; // skip audio
        }

        AudioSource source;
        if (_availableSources.Count > 0)
        {
            source = _availableSources.Pop();
        }
        else
        {
            source = Instantiate(sourcePrefab, transform);
        }

        _activeSources[id].Add(source);
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;

        source.Play();
        StartCoroutine(WaitForEnd(source, id));
    }

    IEnumerator WaitForEnd(AudioSource source, int id)
    {
        while (source.isPlaying)
        {
            yield return null;
        }

        _activeSources[id].Remove(source);
        _availableSources.Push(source);
    }
}