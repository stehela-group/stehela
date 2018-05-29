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
    public int getDamage()
    {
        return this.damage;
    }
}