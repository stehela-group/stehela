using UnityEngine;
using System.Collections;


public class CBackgroundFloor : CSprite
{
    public CBackgroundFloor()
    {
        setImage(Resources.Load<Sprite>("Sprites/tilesBackground/floor"));
        // Define el nombre del background cuando se crea.
        setName("Background_floor");

        // Define en que capa va el background.
        setSortingLayerName("Background");
        render();
    }
}