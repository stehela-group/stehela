class Skill : CSprite
{

    const int CASTING = 1;
    const int REACHING = 2;
    const int AFFECTING = 3;
    const int FINISHED = 4;
    

    public int damage = 0;
    public int castTime = 0;
    public int reachTime = 0;
    public int affectTime = 0;

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