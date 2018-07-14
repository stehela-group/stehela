using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy2 : BattleEntity
{
    public  Enemy2()
    {
        this.setName("prototype_minion");
        setFrames(Resources.LoadAll<Sprite>("Sprites/minionBoss"));
        setScale(3);
        this.setMaxHealth(150);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(10);
        this.skills.Add(new Atacar());
        //this.setXY(1500, 50);
        setState(IDLE);
        setXY(1000, 60);

    }

    override public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty)
    {
        Skill skill = this.skills[CMath.randomIntBetween(0, this.skills.Count - 1)];

        return new Action(this, skill, playerParty[CMath.randomIntBetween(0, playerParty.Count - 1)]);
    }
    override public void setState(int aState)
    {
        base.setState(aState);

        if (aState == BattlePlayer.ATTACKING)
        {
            this.initAnimation(5, 17, 16, false);

        }

        if (aState == BattlePlayer.IDLE)
        {
            this.initAnimation(1, 4, 8, true);
        }
    }
    override public void update()
    {
        base.update();

        if (getState() == IDLE)
        {

        }
        else if (getState() == ATTACKING)
        {

        }
    }

    override public void render()
    {
        base.render();
    }

    override public void destroy()
    {
        base.destroy();

    }
}