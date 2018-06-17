using System.Collections.Generic;

public class Enemy2 : BattleEntity
{
    public  Enemy2()
    {
        this.setName("prototype_minion");

        this.setMaxHealth(125);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(10);
        this.skills.Add(new Atacar());
        this.setXY(1500, 50);
    }

    override public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty)
    {
        Skill skill = this.skills[CMath.randomIntBetween(0, this.skills.Count - 1)];

        return new Action(this, skill, playerParty[CMath.randomIntBetween(0, playerParty.Count - 1)]);
    }
}