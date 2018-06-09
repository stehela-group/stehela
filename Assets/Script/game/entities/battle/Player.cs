using UnityEngine;
using System.Collections;

public class BattlePlayer : BattleEntity
{
    private float initialX;

	public BattlePlayer()
	{
		this.setName("Player");

        this.setMaxHealth(150);
        this.setAttackDamage(30);


		this.skills.Add(new Atacar());
		this.skills.Add(new Hielo());
        this.skills.Add(new Envenenar());

        setFrames (Resources.LoadAll <Sprite>("Sprites/player"));
        setName("Player");
        setScale(5);
        setSortingLayerName("Default");
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        setXY(478, 784);
        this.initialX = this.getX();
        setFlip(true);

    }

    override public void update()
    {
        base.update();


        switch (this.getState())
        {
            case ATTACKING:
                if (getX() >= this.initialX + 50)
                {
                    this.setVelX(-50);

                }

                if (getX() <= this.initialX - 50)
                {
                    this.setVelX(50);

                }

                break;
                  

            default:
                break;
        }

    }
    override public void render()
	{
		base.render ();
	}

    override public void destroy()
    {
        base.destroy();
        
    }

    override public void setState(int aState)
    {
        base.setState(aState);

        if(aState == BattlePlayer.ATTACKING)
        {
            this.setVelX(50);
        }

        if (aState == BattlePlayer.IDLE)
        {
            setX(initialX);
            this.setVelX(0);
        }
    }

    private void move()
    {
        if (CKeyboard.pressed(CKeyboard.LEFT))
        {
            setVelX(-50);
        }
        if (CKeyboard.pressed(CKeyboard.RIGHT))
        {
            setVelX(50);
        }

        if (CKeyboard.pressed(CKeyboard.UP))
        {
            setVelY(50);
        }
        if (CKeyboard.pressed(CKeyboard.DOWN))
        {
            setVelY(-50);
        }
    }
}