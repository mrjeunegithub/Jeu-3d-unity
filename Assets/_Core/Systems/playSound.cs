using System.Collections;
using UnityEngine;

public class playSound : MonoBehaviour
{
    public AudioClip ambiantSound;
        AudioSource audiosource;
    public float volume = 0.15f;
    public bool loop = true;
    public float fadeDuration = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        if (audiosource == null)
        {
            audiosource = gameObject.AddComponent<AudioSource>();
        }
        audiosource.clip = ambiantSound;
        audiosource.loop = loop;
        audiosource.playOnAwake = false;
        if (ambiantSound != null)
        {
            audiosource.volume = 0f;
            audiosource.Play();
            StartCoroutine(FadeInVolume(Mathf.Clamp01(volume), Mathf.Max(0f, fadeDuration)));
        }
        else
            Debug.LogWarning("Aucun son assigné à " + gameObject.name);
    }


    private IEnumerator FadeInVolume(float targetVolume, float duration)
    {
        float startVolume = audiosource.volume;
        if (duration <= 0f)
        {
            audiosource.volume = targetVolume;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }
        audiosource.volume = targetVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
