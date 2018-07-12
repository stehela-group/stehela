using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC2 : CAnimatedSprite
{
    private const int STATE_IDLE = 1;
    private const int STATE_TRANSFORM = 2;
    private const int STATE_NEW_IDLE = 3;
    private string portraitAddress2;
    private  float mTimer = 0;

    private const int WIDTH = 32;
    private const int HEIGHT = 32;
    public COverWorldNPC2()
    {
        
        portraitAddress2 = null;
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC2"));
        setName("Overworld NPC2");
        setSortingLayerName("Personajes");
        setScale(5);

        setWidth(WIDTH * 5);
        setHeight(HEIGHT * 5);
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);
        setState(STATE_IDLE);
        
    }

    override public void update()
    {
        base.update();
        mTimer = mTimer + 1;
       
        if (getState() == STATE_IDLE)
        { 
            if (CLevelState.mOverworldNPC2.collides(CLevelState.mOverworldPlayer))
            {
                setState(STATE_TRANSFORM);
            }
        }
        if (getState() == STATE_TRANSFORM)
        {
           if (mTimer >= 7)
            {
                setState(STATE_NEW_IDLE);
            }
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

    public void mensaje()
    {

        
        string[] text = null;
        if (BattleData.lastBattleOutcome == BattleData.BattleOutcome.NO_BATTLE)
        {
            text = new string[] {
                "HAHA, ES INTERESANTE VER A ALGUIEN CON ASPECTO HUMANOIDE POR ESTOS LADOS. ",
                "PENSÉ QUE USTEDES SE HABIAN EXTINTO AL MOMENTO DE HACER EL NUDO DE ALMAS.",
                "QUE INTERESANTE, INCLUSO LUEGO DE HABER DESTRUIDO EL MUNDO Y HACERNOS VIVIR EN ESTE MUNDO SIN VIDA O MUERTE TODAVÍA SE ATREVEN A SEGUIR EXISTIENDO, JAJAJAJAJAJA, INTERESANTE SIN DUDA."
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

        DialogManager.startDialog(new Dialog(text, portraitAddress2));
    }
    public override void setState(int aState)
    {
        base.setState(aState);

        if (getState() == STATE_IDLE)
        {
            initAnimation(1, 16, 6, true);

        }
        else if (getState() == STATE_TRANSFORM)
        {
            initAnimation(17, 41, 6, false);
        }
        else if(getState() == STATE_NEW_IDLE)
        {
            initAnimation(42, 55, 6, true);
        }

    }
}