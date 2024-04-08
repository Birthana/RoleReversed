using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillDisplay : MonoBehaviour
{
    [SerializeField] private BasicUI timer;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private SpriteRenderer cardSprite;

    public void SetSkill(PlayerSkill skill)
    {
        timer.Display(skill.GetTimer());
        description.text = skill.GetDescription();
        cardSprite.sprite = skill.GetSprite();
    }
}
