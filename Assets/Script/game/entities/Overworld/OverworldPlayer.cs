using UnityEngine;
using System.Collections;

public class COverWorldPlayer : CAnimatedSprite
{
    private const float SPEED = 400.0f;
    

    public COverWorldPlayer()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/worldPlayer"));
        setName("Overworld player");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        
    }

    override public void update()
    {

        base.update();

        if (!CKeyboard.pressed(CKeyboard.UP) && (!CKeyboard.pressed(CKeyboard.DOWN)))
        {
            setVelY(0);
        }
        if ((!CKeyboard.pressed(CKeyboard.LEFT)) && (!CKeyboard.pressed(CKeyboard.RIGHT)))
        {
            setVelX(0);
        }
        if (CKeyboard.pressed(CKeyboard.LEFT))
        {
            setVelX(-SPEED);
        }
        if (CKeyboard.pressed(CKeyboard.RIGHT))
        {
            setVelX(SPEED);
        }
        if (CKeyboard.pressed(CKeyboard.UP))
        {
            setVelY(-SPEED);
        }
        if (CKeyboard.pressed(CKeyboard.DOWN))
        {
            setVelY(SPEED);
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

}
