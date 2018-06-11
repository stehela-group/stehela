using UnityEngine;
using System.Collections;

public class COverWorldPlayer : CAnimatedSprite
{
    

    public COverWorldPlayer()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/player"));
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
            setVelX(-50);
        }
        if (CKeyboard.pressed(CKeyboard.RIGHT))
        {
            setVelX(50);
        }
        if (CKeyboard.pressed(CKeyboard.UP))
        {
            setVelY(-50);
        }
        if (CKeyboard.pressed(CKeyboard.DOWN))
        {
            setVelY(50);
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
