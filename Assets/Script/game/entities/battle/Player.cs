using UnityEngine;
using System.Collections;

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

        setFrames (Resources.LoadAll <Sprite>("Sprites/player"));
        setName("Player");
        setSortingLayerName("Player");
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        setXY(100, 700);
        setFlip(true);

    }
    override public void update()
    {
        base.update();
        
    }
    override public void render()
	{
		base.render ();
	}

    override public void destroy()
    {
        base.destroy();
        
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