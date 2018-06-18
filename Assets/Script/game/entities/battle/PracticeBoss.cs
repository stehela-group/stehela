using System.Collections.Generic;
using UnityEngine;

public class PracticeBoss : BattleEntity
{
    public  PracticeBoss()
    {
        this.setName("Practice Boss");

        this.setMaxHealth(10);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(35);
        setFrames(Resources.LoadAll<Sprite>("Sprites/enemyBoss"));
        initAnimation(1, 8, 8, true);
        setSortingLayerName("Personajes");
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
		setXY(1000, 50);
        setScale(2);

        this.skills.Add(new Atacar());
        this.skills.Add(new Curar());
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
}