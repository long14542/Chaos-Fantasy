using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    private AudioSource audioSource;

    
    // Phát nhạc menu
    public void PlayMenuMusic()
    {
        // Nếu đang phát gameplay music, dừng lại và phát menu music
        if (audioSource.clip != menuMusic)
        {
            audioSource.Stop();
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Phát nhạc gameplay
    public void PlayGameplayMusic()
    {
        // Dừng nhạc menu nếu đang phát
        if (audioSource.clip != gameplayMusic)
        {
            audioSource.Stop();
            audioSource.clip = gameplayMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Dừng nhạc
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
