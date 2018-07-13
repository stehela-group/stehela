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
    private string portraitAddress;
    private  float mTimer = 0;

    private const int WIDTH = 32;
    private const int HEIGHT = 32;
    public COverWorldNPC2()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/NPC2"));
        portraitAddress = "Sprites/animatedPortraitNPC2";
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
                "Una cara nueva, es raro ver caras nuevas hoy en dia.. ",
                "Te gusta el mural que está en mi espalda? Representa la era ",
                "mortal y a los 2 grandes clanes que protegían el árbol de la ",
                "muerte llamado los Tifus  y el árbol de la vida llamados lo Cires,",
                "aquella época donde la vida y la muerte seguían existiendo.. ",
                "Solo han pasado 50 años y parece como si fuera una eternidad.. ",
                "Nose porque te cuento esto, por tu vista parecería que no lo supieras.. ",
                " Pero desde que acabó la era mortal no ha habido nacimientos, así que no tendría sentido que no lo supieras"
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

        DialogManager.startDialog(new Dialog(text, portraitAddress));
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