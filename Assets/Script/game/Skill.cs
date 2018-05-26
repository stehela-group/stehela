class Skill : CSprite
{

    public const int CASTING = 1;
    public const int REACHING = 2;
    public const int AFFECTING = 3;
    public const int FINISHED = 4;
    

    protected int damage = 0;
    protected int castTime = 0;
    protected int reachTime = 0;
    protected int affectTime = 0;

    protected int pepe = 0;

    public Skill() 
    {
    }

    override public void update()
    {
        base.update();

        switch (this.getState())
        {
            case (Skill.CASTING):
                //Establish the Skill Function
                break;
            case (Skill.REACHING):
                break;
            case (Skill.AFFECTING):
                break;
            case (Skill.FINISHED):
                break;
        }
    }

}