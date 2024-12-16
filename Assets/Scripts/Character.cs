using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Health health;
    protected Damage damage;
    protected SoundManager soundManager;
    private readonly string DAMAGE_SOUND_PATH = "Music/Damage";
    private AudioClip damageSound;

    public virtual void Awake() { }

    public Damage GetDamageComponent()
    {
        if (damage == null)
        {
            damage = GetComponent<Damage>();
        }

        return damage;
    }

    public Health GetHealthComponent()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        return health;
    }

    public SoundManager GetSoundManager()
    {
        if (soundManager == null)
        {
            soundManager = GetComponent<SoundManager>();
        }

        return soundManager;
    }

    public AudioClip GetDamageSound()
    {
        if (damageSound == null)
        {
            damageSound = Resources.Load<AudioClip>(DAMAGE_SOUND_PATH);
        }

        return damageSound;
    }

    public void ResetStats()
    {
        GetDamageComponent().ResetDamage();
        GetHealthComponent().RestoreFullHealth();
    }

    public bool IsDead() { return GetHealthComponent().GetCurrentHealth() <= 0; }

    public void TakeDamage(int damage)
    {
        GetSoundManager().Play(GetDamageSound());
        GetHealthComponent().TakeDamage(damage);
    }

    public void TakeDamage(MonsterCardInfo monsterCardInfo) { TakeDamage(monsterCardInfo.damage); }

    public void TakeDamage(MonsterCard monsterCard) { TakeDamage((MonsterCardInfo)monsterCard.GetCardInfo()); }

    public void RestoreHealth(int damage) { GetHealthComponent().RestoreHealth(damage); }

    public int GetDamage() { return GetDamageComponent().GetDamage(); }

    public int GetHealth() { return GetHealthComponent().GetCount(); }

    public int GetMaxHealth() { return GetHealthComponent().GetMaxHealth(); }

    public void IncreaseDamage(int increase) { GetDamageComponent().IncreaseMaxDamage(increase); }

    public void ReduceDamage(int decrease) { GetDamageComponent().DecreaseMaxDamage(decrease); }

    public void TemporaryIncreaseStats(int damage, int health)
    {
        GetDamageComponent().TemporaryIncreaseDamage(damage);
        GetHealthComponent().TemporaryIncreaseHealth(health);
    }

    public void TemporaryDecreaseStats(int damage, int health)
    {
        GetDamageComponent().TemporaryDecreaseDamage(damage);
        GetHealthComponent().TemporaryDecreaseHealth(health);
    }

    public virtual IEnumerator MakeAttack(Character character) { yield return null; }
}
