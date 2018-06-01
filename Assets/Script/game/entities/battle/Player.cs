public class Player : BattleEntity
{
	public Player()
	{
		this.setName("Player");

        this.setMaxHealth(150);
        this.setAttackDamage(30);


		this.skills.Add(new Atacar());
		this.skills.Add(new Hielo());
        this.skills.Add(new Envenenar());

	}

        public init()
        {
        //setFrames(Resources.Loadall<Sprites>("Sprites/player"))
        setName("Player");
        setSortingLayerName("Player");

        }
}