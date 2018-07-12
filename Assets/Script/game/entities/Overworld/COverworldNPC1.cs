using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC1 : CAnimatedSprite
{
    private const int STATE_IDLE = 1;
    private string portraitAddress;
    protected const int WIDTH = 32;
    protected const int HEIGHT = 32;
    public COverWorldNPC1()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC1"));
        portraitAddress = "Sprites/animatedPortraitNPC1";
        setName("Overworld NPC1");
        setSortingLayerName("Personajes");
        setScale(5);

        //se multiplica por 5 por la escala.
        setWidth(WIDTH * 5);
        setHeight(HEIGHT * 5);

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);
        setState(STATE_IDLE);
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
        if (BattleData.lastBattleOutcome == BattleData.BattleOutcome.NO_BATTLE)
        {
            text = new string[] {
                "Que marca peculiar posees en el estómago joven. ",
                "Ten cuidado, hay criaturas peligrosas fuera de esta sala.",
                "Deberás matarlos al mismo tiempo si quieres que no vuelvan a levantarse."
            };
        }
        else if (BattleData.lastBattleOutcome == BattleData.BattleOutcome.WON)
        {
            text = new string[] {
                "¡Los has vencido! Creí que nunca se irían.",
                "*AIU BRBRBRBR* ¿Qué ha sido eso?",
                "Oh no, creo que han venido más ."
            };
        }
        else
        {
            text = new string[] {
                "Te han tirado dentro y quedaste insconsciente...",
                "Hace " + BattleData.battlesLost + " días.",
                "Siguen fuera, no han entrado porque no caben por la puerta."
            };
        }

        DialogManager.startDialog(new Dialog(text, portraitAddress));
    }
    public override void setState(int aState)
    {
        base.setState(aState);

        if (getState() == STATE_IDLE)
        {
            initAnimation(1, 8, 8, true);

        }
    }
}
