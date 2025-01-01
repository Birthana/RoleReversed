using UnityEngine;

public class PlayerSoulCounter : Counter
{
    public int GetCurrentSouls() { return GetCount(); }

    public void IncreaseSouls()
    {
        IncreaseCount(1);
        PlaySound();
    }

    private void PlaySound()
    {
        var audioClip = Resources.Load<AudioClip>("Music/Player_Soul_Sound");
        GetComponent<SoundManager>().Play(audioClip);
    }

    public void DecreaseSouls() { DecreaseCount(1); }

    public void DecreaseSouls(int souls) { DecreaseCount(souls); }
}
