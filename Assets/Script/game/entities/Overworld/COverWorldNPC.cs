using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPC : CAnimatedSprite
{

    protected CText dialog = new CText("");


    public COverWorldNPC()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/tile000"));
        setName("Overworld NPC");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);


        this.dialog.setColor(Color.white);
        this.dialog.setAlignment(TextAlignmentOptions.Left);
        this.dialog.setFontSize(450f);
        this.dialog.setWrapping(false);
        setScale(2);

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


        this.dialog.setText("dialogo");
        this.dialog.setXY(0, 0);
        this.dialog.setSortingLayerName("Dialog");

    }




}
