public class Player : BattleEntity
{
	public Player(string characterName, int characterMaxHealth) : base(characterName, characterMaxHealth)
	{
		Skill s = new Skill();
		s.setSkillName("Skill 1");
		s.setDamage(10);
		this.skills.Add(s);
	}
}