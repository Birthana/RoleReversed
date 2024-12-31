using UnityEngine;
using System;
using System.Collections;

public class Monster : Character
{
    private event Action OnLock;
    private event Action OnUnlock;

    public bool isTemporary = false;
    public MonsterCardInfo cardInfo;
    private DamageAnimation damageAnim;
    private bool isHighlighted = false;
    private Room origin;
    private bool isAssigned = false;
    private MoveAnimation moveAnimation;

    private MoveAnimation GetMoveAnimation()
    {
        if (moveAnimation == null)
        {
            moveAnimation = FindObjectOfType<MoveAnimation>();
        }

        return moveAnimation;
    }

    public void AddToOnLock(Action func) { OnLock += func; }
    public void AddToOnUnlock(Action func) { OnUnlock += func; }

    public void Setup(MonsterCardInfo monsterCardInfo)
    {
        cardInfo = monsterCardInfo;
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = cardInfo.fieldSprite;
        AddToGlobal();
        Setup(cardInfo.GetDamage(), cardInfo.GetHealth());
        damageAnim = new DamageAnimation(spriteRender, Color.green, 0.01f);
    }

    public void Setup(int damage, int health)
    {
        if (cardInfo is TemporaryMonster)
        {
            isTemporary = true;
        }

        SetMaxStats(damage, health);
    }

    public void SetMaxStats(int damage, int health)
    {
        GetDamageComponent().maxCount = damage;
        GetHealthComponent().maxCount = health;
        GetDamageComponent().ResetDamage();
        GetHealthComponent().RestoreFullHealth();
    }

    public void IncreaseStats(int damage, int health)
    {
        GetDamageComponent().IncreaseMaxDamageWithoutReset(damage);
        GetHealthComponent().IncreaseMaxHealthWithoutReset(health);
    }

    public void AddToGlobal()
    {
        if (cardInfo != null)
        {
            cardInfo.Global(this);
        }
    }

    public void Entrance()
    {
        if (cardInfo != null)
        {
            cardInfo.Entrance(this);
            FindObjectOfType<GlobalEffects>().Entrance(this);
        }
    }

    public void Engage()
    {
        if (cardInfo != null)
        {
            var input = new EffectInput(FindObjectOfType<Player>(),
                                        GetCurrentRoom(),
                                        transform.position,
                                        this);
            cardInfo.Engage(input);
            FindObjectOfType<GlobalEffects>().Engage(input);
        }
    }

    public void Exit()
    {
        if (cardInfo != null)
        {
            cardInfo.Exit(this);
            FindObjectOfType<GlobalEffects>().Exit(this);
        }
    }

    public override IEnumerator MakeAttack(Character character)
    {
        yield return PlayAttackAnimation();
        PlaySound();
        character.TakeDamage(GetDamage());
        var spriteRender = character.GetComponent<SpriteRenderer>();
        if(spriteRender != null)
        {
            var damageAnimation = new DamageAnimation(spriteRender, Color.red, 0.1f);
            yield return StartCoroutine(damageAnimation.AnimateFromStartToEnd());
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(damageAnimation.AnimateFromEndToStart());
        }

        Engage();
        if (character.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            yield break;
        }
    }

    private IEnumerator PlayAttackAnimation()
    {
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        hoverAnimation.Hover(transform, new Vector2(-0.5f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        yield return new WaitForSeconds(0.1f);
    }

    public bool IsHighlighted() { return isHighlighted; }

    public void Highlight()
    {
        if (!damageAnim.IsAtStart())
        {
            return;
        }

        isHighlighted = true;
        StartCoroutine(damageAnim.AnimateFromStartToEnd());
    }

    public void UnHighlight()
    {
        StartCoroutine(damageAnim.AnimateFromEndToStart());
        isHighlighted = false;
    }

    public Room GetCurrentRoom() { return transform.GetComponentInParent<Room>(); }

    public void MoveTo(Room newRoom)
    {
        GetCurrentRoom().LeaveEffectCapacity(this);
        transform.SetParent(newRoom.transform);
        newRoom.Enter(this);
    }

    public void Pull(Room newRoom)
    {
        if (origin == null)
        {
            origin = GetCurrentRoom();
            FindObjectOfType<GameManager>().AddToTempMove(this);
        }

        PlayMoveAnimation(newRoom, PullDelayed);
    }

    private void PullDelayed(Room newRoom)
    {
        newRoom.DisplayMonsters();
        TemporaryLeave();
        transform.SetParent(newRoom.transform);
        newRoom.Add(this);
        Entrance();
    }

    public void Push(Room newRoom)
    {
        if (origin == null)
        {
            origin = GetCurrentRoom();
            FindObjectOfType<GameManager>().AddToTempMove(this);
        }

        PlayMoveAnimation(newRoom, PushDelayed);
    }

    private void PushDelayed(Room newRoom)
    {
        newRoom.DisplayMonsters();
        Exit();
        TemporaryLeave();
        transform.SetParent(newRoom.transform);
        newRoom.Add(this);
    }

    private void PlayMoveAnimation(Room newRoom, Action<Room> func)
    {
        if (FindObjectOfType<GameManager>().IsRunning())
        {
            func(newRoom);
            return;
        }

        GetMoveAnimation().MoveMonster(this, newRoom, func);
    }

    private void TemporaryLeave()
    {
        if (HasMoved())
        {
            GetCurrentRoom().Leave(this);
            return;
        }

        GetCurrentRoom().LeaveTemporary(this);
    }

    public Vector3 GetCurrentPosition() { return transform.position; }

    public void ResetToOriginRoom()
    {
        GetCurrentRoom().Leave(this);
        origin.Add(this);
        origin = null;
        new ChangeSortingLayer(gameObject).SetToDefault();
    }

    public bool IsAssigned() { return isAssigned; }

    private void SetAssigned(bool assign) { isAssigned = assign; }

    public void Lock()
    {
        SetAssigned(true);
        OnLock?.Invoke();
    }

    public void Unlock()
    {
        SetAssigned(false);
        OnUnlock?.Invoke();
    }

    public void BecomeTemp()
    {
        if (isTemporary)
        {
            return;
        }

        isTemporary = true;
        GetCurrentRoom().IncreaseCapacity(1);
    }

    public bool HasMoved() { return origin != null; }

    public void PlaySound()
    {
        GetComponent<SoundManager>().Play(cardInfo.GetSound());
    }
}
