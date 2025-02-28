using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public float fadeDuration = 5.0f;   // Durée du fondu croisé
    private float lastTime = 0f;      // Dernier temps de mise à jour
    private float timeBeforeSwitch = 30.0f; // Temps avant de changer de piste
    private static readonly List<string> melodies = new() { "Lena_Raine-Creator", "C418-Aria_Math" };
    private AudioSource currentSource = null;
    private AudioSource nextSource = null;
    private readonly float volumeLayer = 1.0f;

    public static string ChooseRandomTrackMelody()
    {
        return melodies[UnityEngine.Random.Range(0, melodies.Count)];
    }

    internal override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
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

        if (nextSource != null)
        {
            Destroy(currentSource);
            currentSource = nextSource;
            nextSource = null;
        }

        nextSource = gameObject.AddComponent<AudioSource>();
        nextSource.clip = Resources.Load<AudioClip>($"Audio/{name}");
        nextSource.volume = 0f;
        nextSource.loop = true;
        nextSource.playOnAwake = true;
        nextSource.Play();

        if (currentSource != null)
        {
            StartCoroutine(FadeTracks(currentSource, nextSource));
        }
        else
        {
            nextSource.volume = volumeLayer;
            currentSource = nextSource;
            timeBeforeSwitch = currentSource.clip.length - fadeDuration;
            nextSource = null;
        }
    }

    private System.Collections.IEnumerator FadeTracks(AudioSource fromSource, AudioSource toSource)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            if (fromSource != null)
            {
                fromSource.volume = Mathf.Lerp(volumeLayer, 0f, t);
            }
            if (toSource != null)
            {
                toSource.volume = Mathf.Lerp(0f, volumeLayer, t);
            }

            yield return null;
        }

        if (fromSource != null)
        {
            fromSource.Stop();
            Destroy(fromSource);
        }
        currentSource = toSource;
        timeBeforeSwitch = currentSource.clip.length - fadeDuration;
        nextSource = null;
    }
}
