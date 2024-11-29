using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vr_ps02_play_audio_ambulance : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource; // Referencia al AudioSource que contiene el sonido que quieres reproducir

    void Start()
    {
        // Asegurarse de que se ha asignado un AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("No se ha asignado un AudioSource en el inspector.");
        }
    }

    void OnEnable()
    {
        // Verificar si hay un AudioSource y reproducir el sonido si es así
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
