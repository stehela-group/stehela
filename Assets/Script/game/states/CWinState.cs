using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWinState : CGameState
{

    //private CSprite winPortrait;

    public const int IN_PROGRESS = 0;
    public const int FINISHED = 1;

    private CPortrait portrait;

    override public void init()
    {
        setState(CWinState.IN_PROGRESS);
        DialogManager.init();
        portrait = new CPortrait();
        portrait.setXY(200, 200);
        portrait.setSortingLayerName("Default");
        //winPortrait.setImage(Resources.Load<Sprite>("Sprites/winPortrait/winPortrait"));
        //winPortrait.setName("portrait");
        ///winPortrait.setXY(200, 200);
        // Define en que capa va el background.
        //winPortrait.setSortingLayerName("Default");
        string[] text = null;
        text = new string[] {
                "Has derrotado a tus enemigos y has podido recuperar el arbol de la vida.",
        "Presiona \"Espacio\" para poder volver al menu inicial"
            };
        DialogManager.startDialog(new Dialog(text));
    }
    // Update is called once per frame
    public override void update()
    {
        base.update();
        DialogManager.update();
        //winPortrait.update();
        if (CKeyboard.pressed(CKeyboard.SPACE))
        {
            this.setState(FINISHED);
        }
        portrait.update();
    }
    public override void render()
    {
        base.render();
        DialogManager.render();
        portrait.render();
        //winPortrait.render();
    }
    public override void destroy()
    {
        base.destroy();
        DialogManager.destroy();
        portrait.destroy();
        portrait = null;
        //winPortrait.destroy();
        //winPortrait = null;
    }
}