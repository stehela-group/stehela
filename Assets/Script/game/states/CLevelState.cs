using UnityEngine;
using System.Collections;

public class CLevelState : CGameState
{
    private COverWorldPlayer mOverworldPlayer;
    private CTileMap mMap;

    public const int IN_PROGRESS = 0;
    public const int FINISHED = 1;
    
    private CBackgroundFloor mBackgroundFloor;
    private COverWorldNPC mOverworldNPC;

    private CSprite shadow;
    public CLevelState()
	{
	}

	override public void init()
	{
        base.init();
        mMap = new CTileMap();
         
        setState(CLevelState.IN_PROGRESS);
        //mBackground = new CBackground();
        //mBackground.setXY(0, 0);

        
        mOverworldPlayer = new COverWorldPlayer();
        mOverworldPlayer.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
        mOverworldPlayer.setXY(300, 300);

        mOverworldNPC = new COverWorldNPC();
        mOverworldNPC.setXY(CGameConstants.SCREEN_WIDTH - 100, CGameConstants.SCREEN_HEIGHT / 2);
        mOverworldNPC.setXY(500, 300);

        /*CGame.inst().setPlayer(mPlayer);*/

        mBackgroundFloor = new CBackgroundFloor();
        mBackgroundFloor.setXY(0, 0);
        mBackgroundFloor.setSortingLayerName("Background");

        shadow = new CSprite();
        shadow.setImage(Resources.Load<Sprite>("Sprites/dialogShadow/shadow"));
        shadow.setName("Background_shadow");
        shadow.setSortingLayerName("UI");
        shadow.setXY(0, 0);
        shadow.setVisible(false);
    }

	override public void update()
	{
        base.update();
        mMap.update();
       // mBackground.update();



        mOverworldPlayer.update();
        mOverworldNPC.update();




        if (this.getState() == CLevelState.IN_PROGRESS)
        {
            if (mOverworldPlayer.getX() > 500)
            {
                shadow.setVisible(false);
                this.setState(CLevelState.FINISHED);

                return;
            }
        }


        if (mOverworldNPC.collides(mOverworldPlayer))
        {
            
            shadow.setVisible(true);
            
            mOverworldNPC.mensaje();

        }


    }

	override public void render()
	{
        base.render();
        mMap.render();
        //mBackground.render();
        mOverworldPlayer.render();
        mOverworldNPC.render();
    }

	override public void destroy()
	{
        base.destroy();
        mMap.destroy();
        mMap = null;
        mBackgroundFloor.destroy();
		mBackgroundFloor = null;

        mOverworldPlayer.destroy();
        mOverworldPlayer = null;
        mOverworldNPC.destroy();
        mOverworldNPC = null;
    }
}
