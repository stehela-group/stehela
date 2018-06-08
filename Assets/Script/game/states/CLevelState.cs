using UnityEngine;
using System.Collections;

public class CLevelState : CGameState
{
    private CBackground mBackground;
    private COverWorldPlayer mOverworldPlayer;
    private CTileMap mMap;

    public CLevelState()
	{
	}

	override public void init()
	{
        base.init();
        mMap = new CTileMap();
        mBackground = new CBackground();
        mBackground.setXY(0, 0);

        mOverworldPlayer = new COverWorldPlayer();
        mOverworldPlayer.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
        mOverworldPlayer.setXY(300, 300);

        /*CGame.inst().setPlayer(mPlayer);*/
    }

	override public void update()
	{
        base.update();
        mMap.update();
        mBackground.update();
        mOverworldPlayer.update();


    }

	override public void render()
	{
        base.render();
        mMap.render();
        mBackground.render();
        mOverworldPlayer.render();
    }

	override public void destroy()
	{
        base.destroy();
        mMap.destroy();
        mMap = null;
        mBackground.destroy();
        mBackground = null;

        mOverworldPlayer.destroy();
        mOverworldPlayer = null;
    }
}
