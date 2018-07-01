using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC : CAnimatedSprite
{
    private string portraitAddress;

    public COverWorldNPC()
    {
        portraitAddress = "Sprites/NPC/Kairus_Portrait";
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC/KairusWorld"));
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
		string[] text = null;
        if(BattleData.lastBattleOutcome == BattleData.BattleOutcome.NO_BATTLE)
        {
            text = new string[] {
                "Que marca peculiar posees en el estómago joven. ",
                "Ten cuidado, hay criaturas peligrosas fuera de esta sala.",
                "Deberás matarlos al mismo tiempo si quieres que no vuelvan a levantarse."
            };
        }
        else if(BattleData.lastBattleOutcome == BattleData.BattleOutcome.WON)
        {
			text = new string[] {
				"¡Los has vencido! Creí que nunca se irían.",
				"*AIU BRBRBRBR* ¿Qué ha sido eso?",
				"Oh no, creo que han venido más."
			};
        }
        else {
			text = new string[] {
				"Te han tirado dentro y quedaste insconsciente...",
                "Hace " + BattleData.battlesLost + " días.",
                "Siguen fuera, no han entrado porque no caben por la puerta."
			};
        }

        DialogManager.startDialog(new Dialog(text, portraitAddress));
    }
    
}


