using UnityEngine;
using System.Collections;


public class CBackground : CSprite
{
    public CBackground()
    {
        setImage(Resources.Load<Sprite>("Sprites/Background/game_background_2"));
        // Define el nombre del background cuando se crea.
        setName("Background_Battle");

        // Define en que capa va el background.
        setSortingLayerName("Background");
        render();
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