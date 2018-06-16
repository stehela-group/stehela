using UnityEngine;
using System.Collections;

public class COverWorldNPC : CAnimatedSprite
{


    public COverWorldNPC()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/player"));
        setName("Overworld NPC");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);

    }

    override public void update()
    {

        base.update();
        


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
