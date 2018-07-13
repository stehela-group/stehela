using UnityEngine;
public class Companion1 : BattleEntity
{
	public Companion1()
	{
		this.setName("Kairus");
        setFrames(Resources.LoadAll<Sprite>("Sprites/battleAlly"));

        this.setMaxHealth(120);
        this.setHealth(getMaxHealth());
        this.setAttackDamage(20);
        this.setXY(27, 204);
        this.setScale(4);

        this.skills.Add(new Atacar());
		this.skills.Add(new Curar());

        this.slash.setFlip(true);
        this.slash.setXY(slash.getX() + 30, slash.getY() + 30);
    }
}