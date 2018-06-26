using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC : CAnimatedSprite
{

    public COverWorldNPC()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC/KairusWolrd"));
        setName("Overworld NPC");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);
        setScale(4);
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
            "Que marca peculiar posees en el estómago joven.. ",
            "Este viejo recuerda como si fuera ayer el dia que ",
            "esas raíces fueron cortadas por aquellos que juraron protegerlas.",
            "El gran dia 50 años atrás fue la última vez que este viejo vio algo similar.. ",
            "Y pensar que volverían a aparecer 50 años después tan cerca del lugar del suceso.."

        });
    }

}


