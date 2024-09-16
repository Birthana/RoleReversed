using UnityEngine;
using System.Collections;
using TMPro;


public class Monster : Character
{
    private readonly string EFFECT_ICON_PREFAB_FILE_PATH = "Prefabs/EffectIcon";
    private readonly string PULL_INDICATOR_PREFAB_FILE_PATH = "Prefabs/PullIndicator";
    private readonly string PUSH_INDICATOR_PREFAB_FILE_PATH = "Prefabs/PushIndicator";

    public bool isTemporary = false;
    public MonsterCardInfo cardInfo;
    private DamageAnimation damageAnim;
    private bool isHighlighted = false;
    private Room origin;

    public void Setup(MonsterCardInfo monsterCardInfo)
    {
        cardInfo = monsterCardInfo;
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = cardInfo.fieldSprite;
        AddToGlobal();
        Entrance();
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
        GetDamageComponent().ResetDamage();
        GetHealthComponent().RestoreFullHealth();
    }

    private void SetMaxStats(int damage, int health)
    {
        GetDamageComponent().maxCount = damage;
        GetHealthComponent().maxCount = health;
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
            cardInfo.Engage(this, null);
            FindObjectOfType<GlobalEffects>().Engage(this);
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

    public IEnumerator Attack(Character character)
    {
        yield return PlayAttackAnimation();
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

        TemporaryLeave();
        transform.SetParent(newRoom.transform);
        newRoom.Add(this);
        Entrance();
        SpawnPullIndicator(GetCurrentPosition());
    }

    public void Push(Room newRoom)
    {
        if (origin == null)
        {
            origin = GetCurrentRoom();
            FindObjectOfType<GameManager>().AddToTempMove(this);
        }

        SpawnPushIndicator(GetCurrentPosition());
        Exit();
        TemporaryLeave();
        transform.SetParent(newRoom.transform);
        newRoom.Add(this);
    }

    private void SpawnPullIndicator(Vector3 position)
    {
        var pullIndicatorPrefab = Resources.Load<GameObject>(PULL_INDICATOR_PREFAB_FILE_PATH);
        var pullIndicator = Instantiate(pullIndicatorPrefab);
        pullIndicator.transform.position = position;
        Destroy(pullIndicator, 0.5f);
    }

    private void SpawnPushIndicator(Vector3 position)
    {
        var pushIndicatorPrefab = Resources.Load<GameObject>(PUSH_INDICATOR_PREFAB_FILE_PATH);
        var pushIndicator = Instantiate(pushIndicatorPrefab);
        pushIndicator.transform.position = position;
        Destroy(pushIndicator, 0.5f);
    }

    private void TemporaryLeave()
    {
        if (MonsterHasMoved())
        {
            GetCurrentRoom().Leave(this);
            return;
        }

        GetCurrentRoom().LeaveTemporary(this);
    }

    private bool MonsterHasMoved() { return origin != null; }

    public Vector3 GetCurrentPosition() { return transform.position; }

    public void SpawnEntranceIcon()
    {
        SpawnEffectIcon(GetCurrentPosition(), "Entrance");
    }

    public void SpawnExitIcon()
    {
        SpawnEffectIcon(GetCurrentPosition(), "Exit");
    }

    public void SpawnEngageIcon()
    {
        SpawnEffectIcon(GetCurrentPosition(), "Engage");
    }

    private void SpawnEffectIcon(Vector3 position, string iconName)
    {
        var damageNumberPrefab = Resources.Load<GameObject>(EFFECT_ICON_PREFAB_FILE_PATH);
        var damageNumber = Instantiate(damageNumberPrefab);
        damageNumber.transform.position = position;
        damageNumber.GetComponent<TextMeshPro>().text = $"{new EffectText().GetText($"{iconName}")}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2) * 100);
        Destroy(damageNumber, 0.5f);
    }

    public void ResetToOriginRoom()
    {
        GetCurrentRoom().Leave(this);
        origin.Add(this);
        origin = null;
        new ChangeSortingLayer(gameObject).SetToDefault();
    }
}
