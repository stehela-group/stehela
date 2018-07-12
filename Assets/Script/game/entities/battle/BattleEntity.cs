using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    protected CText receivedDamage = new CText("");

    protected CAnimatedSprite slash = new CAnimatedSprite();

    protected bool slashFinished = false;

    protected bool showDamage = false;

    protected int damageReceived = 0;

    protected int amountOfVibrations = 0;

    protected bool vibrate = false;

    protected bool firstTimeSettingUp = true;

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
    public BattleEntity() 
    {
		this.lifeText.setColor(Color.white);
		this.lifeText.setAlignment(TextAlignmentOptions.Left);
		this.lifeText.setFontSize(450f);
        this.lifeText.setWrapping(false);

        

        this.receivedDamage.setColor(Color.red);
        this.receivedDamage.setAlignment(TextAlignmentOptions.Right);
        this.receivedDamage.setFontSize(450f);
        this.receivedDamage.setWrapping(false);
        this.receivedDamage.setVisible(false);

        this.slash.setFrames(Resources.LoadAll<Sprite>("Sprites/animationSlash"));
        this.slash.setName("slash");
        this.slash.setSortingLayerName("UI");
        

        this.slash.gotoAndPlay(1);
        this.slash.stopAnimation();
        this.slash.setVisible(false);
    }

    override public void update()
    {
        base.update();

        if (firstTimeSettingUp)
        {
            this.lifeText.setXY(this.getX() + 20 /* MARGEN*/, this.getY() - 20 /* MARGEN */);
            firstTimeSettingUp = false;
        }
        this.lifeText.setText("Vida: " + this.currentHealth + "/" + this.maxHealth);
        
        this.lifeText.update();
        
        this.receivedDamage.update();

        this.slash.update();
        if (slash.isEnded())
        {
            slash.setVisible(false);
            //que el player flashee un frame.
            receivedDamage.setText("-" + damageReceived);
            receivedDamage.setXY(this.getX() + 200, this.getY());

            receivedDamage.setVelY(-10);
            receivedDamage.setAccelY(50);
            receivedDamage.setVelX(30);


            receivedDamage.setVisible(true);

            
            showDamage = true;
            slash.stopAnimation();
            slash.setVisible(false);
            slash.gotoAndPlay(1);
        }

        if (showDamage)
        {
            Debug.Log("showDamageTRUU");
            // move text, when finished stop showing
            if (receivedDamage.getX() >= this.getX() + 20 && receivedDamage.getY() >= this.getY() +20 )
            {
                receivedDamage.setVisible(false);
                receivedDamage.setVelX(0);
                receivedDamage.setVelY(0);
                receivedDamage.setAccelY(0);
                startVibration();
                showDamage = false;
                return;
                
            }
            else
            {

            }
        }
        if (vibrate)
        {
            Debug.Log("VIBRO!");
            if (amountOfVibrations < 40)
            {
                if ((amountOfVibrations >= 0 && amountOfVibrations<10) || (amountOfVibrations >= 20 && amountOfVibrations < 30))
                {
                    setX(getX() + 15);
                }
                //if (amountOfVibrations >= 10 && amountOfVibrations < 20 || amountOfVibrations <= 30 && amountOfVibrations < 40) 
                else
                {
                    setX(getX() - 15);
                }
                amountOfVibrations++;
            }
            else
            {
                vibrate = false;
                amountOfVibrations = 0;
            }
        }







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
        this.receivedDamage.render();
        this.slash.render();
        switch (this.getState())
        {
            case (BattleEntity.IDLE):
                //this.gotoAndStop(0);
                break;
            case (BattleEntity.RECEIVING_DAMAGE):
                break;
            case (BattleEntity.ATTACKING):

                //this.gotoAndStop(1);
                break;
            case (BattleEntity.DEAD):
                break;
        }
    }
    override public void destroy()
    {
        base.destroy();

        this.name = null;
        this.maxHealth = 0;
        this.currentHealth = 0;
        this.lifeText.destroy();
        this.lifeText = null;

        this.receivedDamage.destroy();
        this.receivedDamage = null;
        foreach (var skill in this.skillsList)
        {
            skill.destroy();
        }
		this.skillsList = null;

        foreach (var skill in this.availableSkills)
        {
            skill.destroy();
        }
		this.availableSkills = null;

        this.slash.destroy();
        this.slash = null;
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
        else if( health < 0)
        {
            this.currentHealth = 0;
        }
        else
        {
            this.currentHealth = health;
        }
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


    //Logica por default, elige una habilidad random y la castea a aliados o enemigos segun los posibles objetivos.
    virtual public Action decideAction(List<BattleEntity> playerParty, List<BattleEntity> enemyParty) 
    {
		Skill skill = this.skills[CMath.randomIntBetween(0, this.skills.Count - 1)];

		if (skill.getPossibleTargets() == Skill.Target.ALLIES)
		{
			return new Action(this, skill, enemyParty[CMath.randomIntBetween(0, enemyParty.Count - 1)]);
		}
		else if (skill.getPossibleTargets() == Skill.Target.ENEMIES)
		{
			return new Action(this, skill, playerParty[CMath.randomIntBetween(0, playerParty.Count - 1)]);
		}
		else if (skill.getPossibleTargets() == Skill.Target.BOTH)
		{
			Debug.Log("Skill puede ir a ambos lados, la entidad todavia no implementa la logica para seleccionar aca.");
		}

		return null;
    }

    public void getDamaged(int damageText)
    {

        //slash, then glow, then vibrate while number falls.
        if (slash.getFlip())
        {
            slash.setXY(this.getX() + 400, this.getY() + 100);
        }
        else
        {
            slash.setXY(this.getX() + 20, this.getY() + 20);
        }
        slash.setVisible(true);
        slash.initAnimation(1, 12, 25, false);

        damageReceived = damageText;

        

    }
    public void startVibration()
    {
        amountOfVibrations = 0;
        vibrate = true;
    }
    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
}
