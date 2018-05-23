using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTileMap
{
	public const int MAP_WIDTH = 17;
	public const int MAP_HEIGHT = 13;

	public const int TILE_WIDTH = 48;
	public const int TILE_HEIGHT = 48;

	private List<List<CTile>> mMap;

	// Cantidad de tiles que hay.
	private const int NUM_TILES = 6;

	// Array con los sprites de los tiles.
	private Sprite[] mTiles;

	// La pantalla tiene 17 columnas x 13 filas de tiles.
	public static int[][] LEVEL_001 = {
		new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
		new int[] {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
		new int[] {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
		new int[] {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
		new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new int[] {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
		new int[] {0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
		new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
	};

	public CTileMap(int aLevel)
	{
		mTiles = new Sprite [NUM_TILES];
		mTiles [0] = Resources.Load<Sprite> ("Sprites/tiles/tile000");
		mTiles [1] = Resources.Load<Sprite> ("Sprites/tiles/tile001");
		mTiles [2] = Resources.Load<Sprite> ("Sprites/tiles/tile002");
		mTiles [3] = Resources.Load<Sprite> ("Sprites/tiles/tile003");
		mTiles [4] = Resources.Load<Sprite> ("Sprites/tiles/tile004");
		mTiles [5] = Resources.Load<Sprite> ("Sprites/tiles/tile005");

		loadLevel (aLevel);
	}

	public void loadLevel(int aLevel)
	{
		mMap = new List<List<CTile>> ();

		for (int y = 0; y < MAP_HEIGHT; y++) 
		{
			mMap.Add (new List<CTile> ());			

			for (int x = 0; x < MAP_WIDTH; x++) 
			{
				int index = LEVEL_001 [y] [x];
				CTile tile = new CTile(x * TILE_WIDTH, y * TILE_HEIGHT, index, mTiles[index]);
				mMap [y].Add (tile);
			}
		}
	}

	public void update()
	{
		for (int y = 0; y < MAP_HEIGHT; y++) 
		{
			for (int x = 0; x < MAP_WIDTH; x++) 
			{
				mMap [y] [x].update ();
			}
		}
	}

	public void render()
	{
		for (int y = 0; y < MAP_HEIGHT; y++) 
		{
			for (int x = 0; x < MAP_WIDTH; x++) 
			{
				mMap [y] [x].render ();
			}
		}
	}

	public void destroy()
	{
		for (int y = MAP_HEIGHT - 1; y >= 0; y--) 
		{
			for (int x = MAP_WIDTH - 1; x > 0; x--) 
			{
				mMap [y] [x].destroy ();
				mMap [y] [x] = null;
			}
			mMap.RemoveAt (y);
		}

		mMap = null;
	}

	public int getTileIndex(int aX, int aY)
	{
		if (aX < 0 || aX > MAP_WIDTH || aY < 0 || aY > MAP_HEIGHT) 
		{
			return 0;
		} 
		else 
		{
			return mMap [aY] [aX].getTileIndex ();
		}
	}

	/*public CTile getTile(int aX, int aY)
	{
		return mMap [aY] [aX];
	}*/
}
