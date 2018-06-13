public class Companion1 : BattleEntity
{
	public Companion1()
	{
		this.setName("prototype_ally");

        this.setMaxHealth(120);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(20);
        this.setXY(200, 700);



        this.skills.Add(new Atacar());
		this.skills.Add(new Curar());
	}
    override public void render()
    {
        return;
    }
}