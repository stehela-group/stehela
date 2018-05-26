﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Action: CGameObject
{
    protected BattleEntity caster;
    protected Skill skill;
    protected BattleEntity target;

    public const PERFORMING = 0;
    public const FINISHED = 0;

    public void init ()
    {
        
    }
    override public void update ()
    {
        base.update();
        skill.update();
        switch (this.getState())
        {
            case (Action.PERFORMING):
                if (skill.getState() == skill.FINISHED)
                {
                    this.setState(this.FINISHED);
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
