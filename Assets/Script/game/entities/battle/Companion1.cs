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

        setState(IDLE);

        this.slash.setFlip(true);
        this.slash.setXY(slash.getX() + 30, slash.getY() + 30);
    }
    override public void update()
    {
        base.update();

        if (getState() == IDLE)
        {

        }
        else if (getState() == ATTACKING)
        {
        }
    }
    override public void render()
    {
        base.render();
    }

    override public void destroy()
    {
        base.destroy();

    }
    override public void setState(int aState)
    {
        base.setState(aState);

        if (aState == BattlePlayer.ATTACKING)
        {
            //this.initAnimation(10, 26, 16, false);
        }

        if (aState == BattlePlayer.IDLE)
        {
            this.initAnimation(1, 18, 16, true);
        }
    }
}