using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC3 : CAnimatedSprite
{
    private const int STATE_IDLE = 1;
    private string portraitAddress3;

    protected const int WIDTH = 32;
    protected const int HEIGHT = 32;
    public COverWorldNPC3()
    {
        portraitAddress3 = null;
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC3"));
        setName("Overworld NPC3");
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

              "HAHA, ES INTERESANTE VER A ALGUIEN CON ASPECTO HUMANOIDE POR ESTOS LADOS,",
              "PENSÉ QUE USTEDES SE HABIAN EXTINTO AL MOMENTO DE HACER EL NUDO DE ALMAS,",
              "QUE INTERESANTE, INCLUSO LUEGO DE HABER DESTRUIDO EL MUNDO Y HACERNOS",
              "VIVIR EN ESTE MUNDO SIN VIDA O MUERTE TODAVÍA",
              "SE ATREVEN A SEGUIR EXISTIENDO, JAJAJAJAJAJA, INTERESANTE SIN DUDA"




            };
        }
        else if (BattleData.lastBattleOutcome == BattleData.BattleOutcome.WON)
        {
            text = new string[] {
                "¡Los has vencido! Creí que nunca se irían.",
                "*AIU BRBRBRBR* ¿Qué ha sido eso?",
                "Oh no, creo que han venido más."
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

        DialogManager.startDialog(new Dialog(text, portraitAddress3));
    }
    public override void setState(int aState)
    {
        base.setState(aState);

        if (getState() == STATE_IDLE)
        {
            initAnimation(1, 6, 6, true);

        }
    }
}
