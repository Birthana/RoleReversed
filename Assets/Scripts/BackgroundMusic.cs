using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource player;
    public AudioClip buildMusic;
    public AudioClip battleMusic;

    private void Awake()
    {
        player.clip = buildMusic;
        player.Play();
    }

    public void SwitchToBuild()
    {
        StartCoroutine(Play(buildMusic));
    }


    public void SwitchToBattle()
    {
        StartCoroutine(Play(battleMusic));
    }

    private IEnumerator Play(AudioClip clip)
    {
        yield return FadeOut();
        player.volume = 1.0f;
        player.clip = clip;
        player.Play();
    }

    private IEnumerator FadeOut()
    {
        while (player.volume > 0.0f)
        {
            player.volume = Mathf.Max(0, player.volume - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
