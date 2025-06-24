using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        // Tambahkan AudioSource ke GameObject ini
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        // Atur properti AudioSource
        audioSource.clip = Resources.Load<AudioClip>("Audio/MySound"); // Ganti dengan path AudioClip Anda
        audioSource.playOnAwake = false; // Jangan langsung mainkan saat game mulai
        audioSource.loop = false; // Tidak diulang

        // Ganti AudioClip dan mainkan
        audioSource.clip = audioClip1; // Ganti dengan AudioClip yang diinginkan
        audioSource.Play();
    }
}
