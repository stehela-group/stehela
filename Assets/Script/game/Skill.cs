class Skill : CSprite
{

    public const int CASTING = 1;
    public const int REACHING = 2;
    public const int AFFECTING = 3;
    public const int FINISHED = 4;
    

    protected int damage = 0;
    protected int castTime = 3;
    protected int reachTime = 0;
    protected int affectTime = 0;

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
                if (getTimeState > castTime)
                    setState(REACHING);
                break;
            case (Skill.REACHING):
                if (getTimeState > reachTime) 
                setState(AFFECTING);
                break;
            case (Skill.AFFECTING):
                if (getTimeState > affectTime)
                    setState(FINISHED);
                break;
            case (Skill.FINISHED):
                break;
        }
    }

}