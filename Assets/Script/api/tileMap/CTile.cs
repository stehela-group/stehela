using UnityEngine;
using System.Collections;

public class CTile : CSprite
{
	// Tile index. Starting from 0.
	private int mTileIndex;

	// Parametros: coordenada del tile (x, y) y el indice del tile.
	public CTile(int aX, int aY, int aTileIndex, Sprite aSprite)
	{
		setXY (aX, aY);
		setTileIndex(aTileIndex);

		setImage (aSprite);
		setSortingLayerName ("MapExplorer");
	}

	public void setTileIndex(int aTileIndex)
	{
		mTileIndex = aTileIndex;
	}

	public int getTileIndex()
	{
		return mTileIndex;
	}

	override public void render()
	{
		base.render ();
	}

	override public void update()
	{
		base.update ();
	}

	override public void destroy()
	{
		base.destroy();
		this.mTileIndex = 0;
	}
}
