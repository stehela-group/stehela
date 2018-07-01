using UnityEngine;
public class Companion1 : BattleEntity
{
	public Companion1()
	{
		this.setName("prototype_ally");
        setFrames(Resources.LoadAll<Sprite>("Sprites/battleAlly"));

        this.setMaxHealth(120);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(20);
        this.setXY(27, 204);
        this.setScale(4);

        this.skills.Add(new Atacar());
		this.skills.Add(new Curar());
	}
}