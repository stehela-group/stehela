using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Action: CGameObject
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
    }
    override public void update ()
    {
        base.update();
        skill.update();
        switch (this.getState())
        {
            case (Action.PERFORMING):
                if (skill.getState() == Skill.FINISHED)
                {
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
