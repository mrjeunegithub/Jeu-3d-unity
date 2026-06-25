using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Configuration Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Au démarrage, on donne au Slider la valeur actuelle du Mixer
        float valeurActuelle;
        if(audioMixer.GetFloat("MasterVolume", out valeurActuelle))
        {
            //le mixer utilise des décibels (-80 à 20). On convertit vers une valeur entre 0 et 1 pour le slider
            volumeSlider.value = Mathf.Pow(10, valeurActuelle / 20);
        }

        // on ajoute un ecouteur des changements du slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        //on evite le 0 pour ne pas calculer ln(0)
        if(sliderValue <= 0.0001f)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
            return;
        }

        // on  convertis la valeur en décibel avec la fonction log_10
        float decibels = Mathf.Log10(sliderValue) * 20;

        audioMixer.SetFloat("MasterVolume", decibels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
