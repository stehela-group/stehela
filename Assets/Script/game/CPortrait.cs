using UnityEngine;
using System.Collections;


public class CPortrait : CSprite
{
    public CPortrait()
    {
        setImage(Resources.Load<Sprite>("Sprites/winPortrait/winPortrait"));
        // Define el nombre del background cuando se crea.
        setName("portrait");

        // Define en que capa va el background.
        setSortingLayerName("Default");
        render();
        
    }
}