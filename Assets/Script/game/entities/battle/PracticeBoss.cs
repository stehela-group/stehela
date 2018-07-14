using System.Collections.Generic;
using UnityEngine;

public class PracticeBoss : BattleEntity
{
    public  PracticeBoss()
    {


        this.setName("Practice Boss");

        this.setMaxHealth(250);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(35);
        setFrames(Resources.LoadAll<Sprite>("Sprites/enemyBoss"));
        setState(IDLE);
        //render();
        setSortingLayerName("Personajes");
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        //setXY(1100, 60);
        this.setXY(1350, 50);
        setScale(3);

        this.skills.Add(new Atacar());
        //this.skills.Add(new Curar());
        this.skills.Add(new Corromper());
    }




   



    override public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty)
    {
        Skill skill = this.skills[CMath.randomIntBetween(0, this.skills.Count - 1)];

        if (skill.getPossibleTargets() == Skill.Target.ALLIES)
        {
            return new Action(this, skill, enemyParty[CMath.randomIntBetween(0, enemyParty.Count - 1)]);
        }
        else if(skill.getPossibleTargets() == Skill.Target.ENEMIES)
        {
            return new Action(this, skill, playerParty[CMath.randomIntBetween(0, playerParty.Count - 1)]);
        }
        else if(skill.getPossibleTargets() == Skill.Target.BOTH)
        {
            Debug.Log("Skill puede ir a ambos lados, la entidad todavia no implementa la logica para seleccionar aca.");
        }

        return null;


    }
    override public void setState(int aState)
    {
        base.setState(aState);

        if (aState == BattlePlayer.ATTACKING)
        {
            this.initAnimation(9, 23, 8, false);

        }

        if (aState == BattlePlayer.IDLE)
        {
            this.initAnimation(1, 8, 8, true);
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