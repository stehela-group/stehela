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

    //Creates the action, in which a caster is selected, a skill is selected and a target is selected. 
    //The Action state is set to PERFORMING (Choosing a skill) and the Skill state is set to CASTING.
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
                //If the state of Skill is CASTING and the caster state is not Attacking, set the state to Attacking.
                if(skill.getState() == Skill.CASTING && caster.getState() != BattleEntity.ATTACKING)
                {
                    caster.setState(BattleEntity.ATTACKING);
                }
                //If the state of Skill is REACHING (next state of skill after casting) and the caster state is not idle, set the state to idle.
                else if(skill.getState() == Skill.REACHING && caster.getState() != BattleEntity.IDLE)
                {
                    
                    caster.setState(BattleEntity.IDLE);
                }
                //If the state of Skill is AFFECTING (next state of skill after reaching) and the caster state is not RECEIVING_DAMAGE, set the state to RECEIVING_DAMAGE.
                else if (skill.getState() == Skill.AFFECTING && caster.getState() != BattleEntity.RECEIVING_DAMAGE)
                {
                    target.setHealth(target.getHealth() - skill.getDamage());
                    caster.setState(BattleEntity.RECEIVING_DAMAGE);
                }
                //if the Skill is ended
                else if (skill.getState() == Skill.FINISHED)
                {
                    //If the caster health is less or equal than zero, change the caster state to DEAD
                    if(caster.getHealth() <= 0)
                    {
                        caster.setState(BattleEntity.DEAD);
                    }
                    //If the target health is less or equal than zero, change the target state to DEAD
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
