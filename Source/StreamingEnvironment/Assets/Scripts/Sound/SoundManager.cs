using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private float lastTime = 0f;
    private float timeBeforeSwitch = 30.0f;
    private static readonly List<string> melodies = new() { "Lena_Raine-Creator", "C418-Aria_Math" };
    private static readonly AudioSource[] switchSource = new AudioSource[2];
    private static uint currentSourceIndex = 0;
    private static AudioSource CurrentSource => switchSource[currentSourceIndex];
    private static AudioSource NextSource => switchSource[1 - currentSourceIndex];
    private readonly float volumeLayer = 1.0f;

    public static string ChooseRandomTrackMelody()
    {
        return melodies[UnityEngine.Random.Range(0, melodies.Count)];
    }

    internal void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (CurrentSource == null)
        {
            switchSource[0] = gameObject.AddComponent<AudioSource>();
            switchSource[0].volume = volumeLayer;
            switchSource[0].loop = false;
            switchSource[0].playOnAwake = true;
        }

        if (NextSource == null)
        {
            switchSource[1] = gameObject.AddComponent<AudioSource>();
            switchSource[1].volume = 0f;
            switchSource[1].loop = false;
            switchSource[1].playOnAwake = true;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this != null && scene.isLoaded && scene == SceneManager.GetActiveScene())
        {
            PlayMusic(ChooseRandomTrackMelody());
        }
    }

    internal void FixedUpdate()
    {
        if (this != null && Time.time - lastTime > timeBeforeSwitch)
        {
            lastTime = Time.time;
            if (SceneManager.GetActiveScene().isLoaded)
            {
                PlayMusic(ChooseRandomTrackMelody());
            }
        }
    }

    public void PlayMusic(string name)
    {
        if (this == null) return;

        CurrentSource.Stop();
        NextSource.clip = Resources.Load<AudioClip>($"Audio/{name}");
        NextSource.volume = volumeLayer;
        NextSource.Play();

        SwapSources();
        timeBeforeSwitch = CurrentSource.clip.length;
    }

    private void SwapSources()
    {
        currentSourceIndex = 1 - currentSourceIndex;
    }
}
