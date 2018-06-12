using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : CAnimatedSprite
{
    public const int IDLE = 0;
    public const int RECEIVING_DAMAGE = 1;
    public const int ATTACKING = 2;
    public const int DEAD = 3;

    protected int maxHealth = 0;
    protected int currentHealth = 0;

    protected int attackDamage = 0;

    protected bool entityCasting = false;
    protected string name;

    protected CText lifeText = new CText("");

    private List<Skill> skillsList = new List<Skill>();

    private List<Skill> availableSkills = new List<Skill>();

    private Skill selectedSkill;


    protected List<Skill> skills
    {
        get
        {
            return this.skillsList;
        }
    }

    // Use this for initialization
    public BattleEntity() { }

    override public void update()
    {
        base.update();

        this.lifeText.setText("Vida: " + this.currentHealth + "/" + this.maxHealth);
        this.lifeText.setXY(this.getX() - 20, this.getY() - 20 /* MARGEN */);
        this.lifeText.setFontSize(250);
        //this.lifeText.setCanAutoSize(true);
        this.lifeText.update();

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
    

        // limpia la lista de habilidades disponibles
    public void clearAvailableSkills()
    {
        this.availableSkills.Clear();
    }
        // reduce en uno el CD de cada habilidad
    public void reduceCooldowns(int quantity)
    {
        foreach (var skill in this.skillsList)
        {
            if (skill.getCurrentCooldown() > 0)
            {
                skill.loseCurrentCooldownBy(quantity);
            }
        }
    }
        // agrega a la lista de availableSkills las habilidades que no tengan CD
    public void setSelectedAction()
    {
        foreach(var skill in this.skillsList)
        {
            int y;
            y = skill.getCurrentCooldown();
            if (y == 0)
            {
                availableSkills.Add(skill);
            }
        }
        int selection = CMath.randomIntBetween(1, availableSkills.Count);
        //le restamos uno a selection ya que la lista comienza en 0 y termina en su length-1
        selectedSkill = availableSkills[selection - 1];
    }
    public Skill getSelectedAction()
    {
        return selectedSkill;
    }
    public void setTarget(Skill skill, BattleEntity selectedTarget)
    {
        skill.setTarget(selectedTarget);
    }


    override public void render()
    {
        base.render();
        this.lifeText.render();
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

        if (health > this.maxHealth)
        {
            this.currentHealth = this.maxHealth;
        }
        else
        {
            this.currentHealth = health;
        }
        Debug.Log(this.name + " - Vida: " + this.getHealth());
    }

    public int getHealth()
    {
        return this.currentHealth;
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

    public void setAttackDamage(int attack)
    {
        this.attackDamage = attack;
    }

    public int getAttackDamage()
    {
        return this.attackDamage;
    }

    public bool isCasting()
    {
        return this.entityCasting;
    }

    public List<Skill> getSkills()
    {
        return this.skills;
    }


    virtual public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty) 
    {
        return null;
    }
}