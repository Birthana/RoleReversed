using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSound : SoundManager
{
    public List<AudioClip> randomAudios = new List<AudioClip>();

    public void Play() { Play(GetRandomAudioClip()); }

    public void Stop()
    {
        if (!player.isPlaying)
        {
            return;
        }

        StartCoroutine(WaitUntilEndOfClip());
    }

    private IEnumerator WaitUntilEndOfClip()
    {
        queue = new List<AudioClip>();
        yield return new WaitForSeconds(player.clip.length - player.time);
    }

    private AudioClip GetRandomAudioClip()
    {
        return randomAudios[Random.Range(0, randomAudios.Count)];
    }
}
