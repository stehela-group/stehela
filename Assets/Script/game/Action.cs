using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action: CGameObject
{
    protected BattleEntity caster;
    protected Skill skill;
    protected BattleEntity target;

    public const int PERFORMING = 0;
    public const int FINISHED = 1;

    public Action (BattleEntity caster, Skill skill, BattleEntity target)
    {
        this.caster = caster;
        this.skill = skill;
        this.target = target;
        this.setState(Action.PERFORMING);
        this.skill.setState(Skill.CASTING);
    }

    override public void update ()
    {
        base.update();
        skill.update();
        
        switch (this.getState())
        {
            case (Action.PERFORMING):
                if(skill.getState() == Skill.CASTING && caster.getState() != BattleEntity.ATTACKING)
                {
                    caster.setState(BattleEntity.ATTACKING);
                }
                else if(skill.getState() == Skill.REACHING && caster.getState() != BattleEntity.IDLE)
                {
                    caster.setState(BattleEntity.IDLE);
                }
                else if(skill.getState() == Skill.AFFECTING && caster.getState() != BattleEntity.RECEIVING_DAMAGE)
                {
                    target.setHealth(target.getHealth() - skill.getDamage());
                    caster.setState(BattleEntity.RECEIVING_DAMAGE);
                }
                else if (skill.getState() == Skill.FINISHED)
                {
                    if(caster.getHealth() <= 0)
                    {
                        caster.setState(BattleEntity.DEAD);
                    }

                    if(target.getHealth() <= 0)
                    {
                        target.setState(BattleEntity.DEAD);
                    }

                    this.setState(Action.FINISHED);
                }
            break;
            case (Action.FINISHED):
                this.destroy();
                break;
        }
    }
    override public void render ()
    {
        base.render();
        skill.render();
    }
    override public void destroy ()
    {
        base.destroy();
        this.caster = null;
        this.skill = null;
        this.target = null;
    }
}
