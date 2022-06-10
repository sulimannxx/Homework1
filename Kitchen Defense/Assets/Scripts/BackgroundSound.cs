using System.Collections;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [SerializeField] private AudioSource _natureSoundsSource;
    [SerializeField] private AudioSource _backgroundSource;
    [SerializeField] private AudioClip[] _backgroundSounds;

    private void Start()
    {
        _natureSoundsSource.Play();
        StartCoroutine(PlayNextBackgroundSong());
    }

    private IEnumerator PlayNextBackgroundSong()
    {
        _backgroundSource.clip = _backgroundSounds[Random.Range(0, _backgroundSounds.Length)];
        _backgroundSource.Play();
        float songTime = _backgroundSource.clip.length;
        yield return new WaitForSeconds(songTime);
        StartCoroutine(PlayNextBackgroundSong());
    }
}
