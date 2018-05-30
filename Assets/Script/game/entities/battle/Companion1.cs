public class Companion1 : BattleEntity
{
	public Companion1()
	{
		this.setName("Companion 1");
		this.skills.Add(new Atacar());
		this.skills.Add(new Curar());
	}
}