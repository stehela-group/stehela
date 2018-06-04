using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BattleEntity
{
    public  Enemy1()
    {
        this.setName("prototype_Boss");

        this.setMaxHealth(500);
        this.setAttackDamage(35);
        setFrames(Resources.LoadAll<Sprite>("Sprites/enemyBoss"));
        setScale(3);
        setSortingLayerName("Default");
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        setXY(1395, 330);
        Debug.Log("Holi soy el boss ");

        this.skills.Add(new Atacar());
        this.skills.Add(new Curar());
        this.skills.Add(new Corromper());
    }

    override public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty)
    {
        Skill skill = this.skills[CMath.randomIntBetween(0, this.skills.Count - 1)];

        if (skill is Curar)
        {
            return new Action(this, skill, enemyParty[CMath.randomIntBetween(0, enemyParty.Count - 1)]);
        }

        return new Action(this, skill, playerParty[CMath.randomIntBetween(0, playerParty.Count - 1)]);
    }
}