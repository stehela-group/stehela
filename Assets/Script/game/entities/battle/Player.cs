public class Player : BattleEntity
{
	public Player()
	{
		this.setName("Player");
		this.skills.Add(new Atacar());
		this.skills.Add(new Hielo());
	}
}