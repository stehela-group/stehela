using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC : CAnimatedSprite
{

    public COverWorldNPC()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/tile000"));
        setName("Overworld NPC");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);
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

    public void mensaje()
    {
        
        DialogManager.startDialog(new string[] {
            "Pepito",
            "Pepote",
            "Peputo"
        });
    }

}
