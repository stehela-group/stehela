public class Skill : CSprite
{

    public const int CASTING = 1;
    public const int REACHING = 2;
    public const int AFFECTING = 3;
    public const int FINISHED = 4;
    

    protected string name = "";

    // % of damage dealt, depending on the entity attackDamage
    protected int damagePercentage = 0;

    // % of heal dealt, depending on the entity attacDamage
    protected int healPercentage = 0;

    // animation related variables, which determine the skill's current state.
    protected int castTime = 0;
    protected int reachTime = 0;
    protected int affectTime = 0;

    // amount of turns it takes the caster to use ability
    protected int castingTurns = 0;
    // amount of turns the target stays affected by ability
    protected int affectingTurns = 0;

    // amount of turns it takes for the caster to be able to use the ability again
    protected int cooldown = 0;

    protected int currentCooldown = 0;

    //skill attributes that affect the target
    protected bool stun = false;
    protected bool poison = false;
    protected bool attackDebuff = false;

    protected int poisonPercentage = 0;
    protected int attackDebuffPercentage = 0;

    private int[] mFramesCasting = new int[] { };
    private int[] mFramesReaching = new int[] { };
    private int[] mFramesAffecting = new int[] { };


    /*
        TODO: 
            - Variables para guardar las animaciones de casting, reaching, y affecting.
            - Sobreescribir setState para inicializar animaciones de Reaching y Affecting
            - Funcion getDamage que devuelve el daño de la habilidad
            - Animacion por default es la de casting que debería empezar a reproducirse cuando se crea la skill (al menos por ahora)
     */

    public Skill() 
    {
        
    }

    override public void update()
    {
        base.update();

        switch (this.getState())
        {
            case (Skill.CASTING):
                if (this.getTimeState() > this.castTime)
                {
                    this.setState(Skill.REACHING);
                }
                break;
            case (Skill.REACHING):
                if (this.getTimeState() > this.reachTime) 
                {
                    this.setState(Skill.AFFECTING);
                }
                break;
            case (Skill.AFFECTING):
                if (this.getTimeState() > this.affectTime)
                {
                    this.setState(Skill.FINISHED);
                }
                break;
            case (Skill.FINISHED):
                break;
        }
        
    }
    // DAMAGE FUNCTIONS
    public void setDamage(int damage)
    {
        //dividido 100 para ser porcentual y poder utilizarlo para multiplicar el attackDamage del caster para daño
        this.damagePercentage = damage/100;
    } 

    public int getDamage()
    {
        return this.damagePercentage;
    }

    // HEAL FUNCTIONS
    public void setHeal(int heal)
    {
        //dividido 100 para ser porcentual y poder utilizarlo para multiplicar el attackDamage del caster para curar
        this.healPercentage = heal/100;
    }

    public int getHeal()
    {
        return this.healPercentage;
    }

    // ATTRIBUTE FUNCTIONS
    public void setStun (bool aStun)
    {
        this.stun = aStun;
    }

    public void setPoison(bool aPoison)
    {
        this.poison = aPoison;
    }
    public void setPoisonPercentage(int aDamage)
    {
        this.poisonPercentage = aDamage/100;
    }

    public void setAttackDebuff(bool aDebuff)
    {
        this.attackDebuff = aDebuff;
    }
    public void setAttackDebuffPercentage(int aAmount)
    {
        this.attackDebuffPercentage = aAmount/100;
    }



    // NAME FUNCTIONS
    public void setSkillName(string name)
    {
        this.name = name;
        this.setName("Skill - " + this.name);
    }

    public string getSkillName()
    {
        return this.name;
    }

    // TURN RELATED FUNCTIONS
    public void setCastingTurns(int turns)
    {
        this.castingTurns = turns;
    }
    public void setAffectingTurns(int turns)
    {
        this.affectingTurns = turns;
    }

    // COLDDOWN FUNCTIONS
    public void setCooldown(int turns)
    {
        this.cooldown = turns;
    }
    public int getCooldown()
    {
        return this.cooldown;
    }

    public int getCurrentCooldown()
    {
        return currentCooldown;
    }
    public void loseCurrentCooldown()
    {
        this.currentCooldown -= 1;
    }

    // ANIMATION FUNCTIONS
    public void setCastTime (int time)
    {
        this.castTime = time;
    }
    public void setReachTime(int time)
    {
        this.reachTime = time;
    }
    public void setAffectTime (int time)
    {
        this.affectTime = time;
    }
}
    