using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : CSprite 
{
    public const int IDLE = 0;
    public const int RECEIVING_DAMAGE = 1;
    public const int ATTACKING = 2;
    public const int DEAD = 3;

    protected int maxHealth = 0;
    protected int currentHealth = 0;

    protected string name;
    private List<Skill> skillsList = new List<Skill>();

    protected List<Skill> skills
    {
        get
        {
            return this.skillsList;
        }
    }

    // Use this for initialization
    public BattleEntity () {}

    override public void update ()
    {
        base.update();
		switch (this.getState())
        {
            case (BattleEntity.IDLE):
                break;
            case (BattleEntity.RECEIVING_DAMAGE):
                break;
            case (BattleEntity.ATTACKING):
                break;
            case (BattleEntity.DEAD):
                break;
        }
	}
    override public void render ()
    {
        base.render();
        switch (this.getState())
        {
            case (BattleEntity.IDLE):
                break;
            case (BattleEntity.RECEIVING_DAMAGE):
                break;
            case (BattleEntity.ATTACKING):
                break;
            case (BattleEntity.DEAD):
                break;
        }
    }
    override public void destroy()
    {
        base.destroy();

        this.skillsList = null;
        this.name = null;
        this.maxHealth = 0;
        this.currentHealth = 0;
    }

    /// <summary>
    ///     Setea la vida de la entidad
    /// </summary>
    /// <param name="health"> Nueva vida de la entidad </param>
    public void setHealth(int health)
    {
        //this.currentHealth = health > this.maxHealth ? this.maxHealth : health;

        if(health > this.maxHealth)
        {
            this.currentHealth = this.maxHealth;
        }
        else
        {
            this.currentHealth = health;
        }
    }

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int getMaxHealth()
    {
        return this.maxHealth;
    }

    override public void setName(string aName)
    {
        base.setName("BattleEntity - " + aName);
        this.name = aName;
    }

    override public string getName()
    {
        return this.name;
    }

    public List<Skill> getSkills()
    {
        return this.skills;
    }
}