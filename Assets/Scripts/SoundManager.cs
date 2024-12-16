using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    protected AudioSource player;
    protected List<AudioClip> queue = new List<AudioClip>();

    private void Awake()
    {
        player = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayQueued();
    }

    private void PlayQueued()
    {
        if (queue.Count == 0)
        {
            return;
        }

        Play(queue[0]);
        queue.RemoveAt(0);
    }

    public void Play(AudioClip clip)
    {
        if (player.isPlaying)
        {
            queue.Add(clip);
            return;
        }

        player.clip = clip;
        player.Play();
    }
}
