﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class CLevelState : CGameState
{
    public static COverWorldPlayer mOverworldPlayer;
    private CTileMap mMap;

    public const int IN_PROGRESS = 0;
    public const int FINISHED = 1;

    private CBackgroundFloor mBackgroundFloor;
    private COverWorldNPCKairus mOverworldNPCKairus;
    private COverWorldNPC1 mOverworldNPC1;
    public static COverWorldNPC2 mOverworldNPC2;
    public static COverWorldNPC3 mOverworldNPC3;


    private CSprite shadow;

    protected Dictionary<CSprite, CGameObject> shadowDictionary = new Dictionary<CSprite, CGameObject>();


    public CLevelState()
	{
	}

	override public void init()
	{
        base.init();
        mMap = new CTileMap();
        CGame.inst().setMap(mMap);
        setState(CLevelState.IN_PROGRESS);
        //mBackground = new CBackground();
        //mBackground.setXY(0, 0);

        mOverworldPlayer = new COverWorldPlayer();
        //mOverworldPlayer.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
        mOverworldPlayer.setXY(300, 300);
        

        mOverworldNPCKairus = new COverWorldNPCKairus();
        //mOverworldNPC.setXY(CGameConstants.SCREEN_WIDTH - 100, CGameConstants.SCREEN_HEIGHT / 2);
        mOverworldNPCKairus.setXY(1600, 700);

        mOverworldNPC1 = new COverWorldNPC1();
        mOverworldNPC1.setXY(900, 40);

        mOverworldNPC2 = new COverWorldNPC2();
        mOverworldNPC2.setXY(900, 870);

        mOverworldNPC3 = new COverWorldNPC3();
        mOverworldNPC3.setXY(600, 500);

        /*CGame.inst().setPlayer(mPlayer);*/

        mBackgroundFloor = new CBackgroundFloor();
        mBackgroundFloor.setXY(0, 0);
        mBackgroundFloor.setSortingLayerName("Background");

        createShadow(mOverworldPlayer);
        createShadow(mOverworldNPCKairus);
        createShadow(mOverworldNPC1);
        createShadow(mOverworldNPC2);
        createShadow(mOverworldNPC3);

        DialogManager.init();
    }

	override public void update()
	{
        base.update();
        mMap.update();
        
        DialogManager.update();
       // mBackground.update();



        mOverworldPlayer.update();
        mOverworldNPCKairus.update();
        mOverworldNPC1.update();
        mOverworldNPC2.update();
        mOverworldNPC3.update();

        //Debug.Log(shadowDictionary.Count);
        foreach (var entry in this.shadowDictionary)
        {
            Debug.Log(entry.Value.getX() + entry.Value.getWidth() / 2 - entry.Key.getWidth() / 2);
           entry.Key.setXY(entry.Value.getX() + entry.Value.getWidth() / 10-entry.Key.getWidth()/10, entry.Value.getY() + entry.Value.getWidth()  -entry.Key.getHeight()/10);
            
        }

        if (this.getState() == CLevelState.IN_PROGRESS)
        {
            if (mOverworldPlayer.getX() >= CGameConstants.SCREEN_WIDTH)
            {
                this.setState(CLevelState.FINISHED);

                return;
            }
        }
        
        //TODO: Realizar manager de NPC Y chequear colicion con cualquier Npc
        if (mOverworldNPCKairus.collides(mOverworldPlayer))
        {
            //if (DialogManager.init().getDialog() == null)

                mOverworldNPCKairus.mensaje();
        }
        if (mOverworldNPC1.collides(mOverworldPlayer))
        {
            //if (DialogManager.init().getDialog() == null)

            mOverworldNPC1.mensaje();
        }
        if (mOverworldNPC2.collides(mOverworldPlayer))
        {
            //if (DialogManager.init().getDialog() == null)

            mOverworldNPC2.mensaje();
        }

        if (mOverworldNPC3.collides(mOverworldPlayer))
        {
            //if (DialogManager.init().getDialog() == null)

            mOverworldNPC3.mensaje();
        }



    }

	override public void render()
	{
        base.render();
        DialogManager.render();
        mMap.render();
        //mBackground.render();
        mOverworldPlayer.render();
        mOverworldNPCKairus.render();
        mOverworldNPC1.render();
        mOverworldNPC2.render();
        mOverworldNPC3.render();

        foreach (var entity in this.shadowDictionary)
        {
            entity.Key.render();
        }
    }

	override public void destroy()
	{
        base.destroy();
        DialogManager.destroy();
        mMap.destroy();
        mMap = null;
        mBackgroundFloor.destroy();
		mBackgroundFloor = null;

        mOverworldPlayer.destroy();
        mOverworldPlayer = null;
        mOverworldNPCKairus.destroy();
        mOverworldNPCKairus = null;
        mOverworldNPC1.destroy();
        mOverworldNPC1 = null;
        mOverworldNPC2.destroy();
        mOverworldNPC1 = null;


        mOverworldNPC3.destroy();
        mOverworldNPC3 = null;



        foreach (var entity in this.shadowDictionary)
        {
            entity.Key.destroy();
        }
    }

    private void createShadow(CGameObject entity)
    {
        shadow = new CSprite();
        shadow.setName("shadow");
        shadow.setImage(Resources.Load<Sprite>("Sprites/shadow/shadow"));
        // Lo seteamos en los pies del entity ingresado.
        shadow.setSortingLayerName("shadows");
        shadow.setWidth(200);
        shadow.setHeight(80);
        shadowDictionary.Add(shadow, entity);


    }
}
