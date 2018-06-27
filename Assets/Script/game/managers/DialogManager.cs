using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


static public class DialogManager 
{
    static private bool mInitialized = false;
    static private CSprite shadow;
    static private CSprite characterPortrait;
    static private CText text;
    static private string[] dialog;
    static private int currentDialog;

    private static int MARGIN = 80;
	
	public static void init()
	{
		if (mInitialized) 
		{
			return;
		}
		mInitialized = true;

        shadow = new CSprite();
        //Esta tambien el shadow solo que llena toda la pantalla
        shadow.setImage(Resources.Load<Sprite>("Sprites/dialogShadow/shadow"));
        shadow.setName("Dialog - Background");
        shadow.setSortingLayerName("UI");
        shadow.setXY(0, CGameConstants.SCREEN_HEIGHT / 3 * 2);
        shadow.setWidth(CGameConstants.SCREEN_WIDTH);
        shadow.setVisible(false);

        characterPortrait = new CSprite();
        characterPortrait.setName("Character - Portrait");
        characterPortrait.setSortingLayerName("UI");
        characterPortrait.setXY(shadow.getX() + MARGIN, shadow.getY() + MARGIN);
        characterPortrait.setVisible(false);
        characterPortrait.setSortingOrder(1);

        text = new CText("");
        text.setFontSize(500.0f);
        text.setVisible(false);
        text.setXY(CGameConstants.SCREEN_WIDTH / 4 + MARGIN, shadow.getY() + MARGIN);
        text.setWidth(CGameConstants.SCREEN_WIDTH / 4 * 3 - MARGIN * 2);

    }

	public static void update()
	{
        shadow.update();
        text.update();
        characterPortrait.update();


        if (dialog != null)
        {
            if(CKeyboard.firstPress(CKeyboard.ENTER) && dialog.Length > 0 && currentDialog < dialog.Length - 1)
            {
                currentDialog++;
                text.setText(dialog[currentDialog]);
            }
            else if(CKeyboard.firstPress(CKeyboard.ENTER) && currentDialog == dialog.Length - 1)
            {
                dialog = null;
                text.setText("");
                shadow.setVisible(false);
                text.setVisible(false);
                currentDialog = 0;

                characterPortrait.setVisible(false);
            }
        }
	}

    public static void render()
    {
        shadow.render();
        text.render();
        characterPortrait.render();
    }

    public static void startDialog(string[] textos, string picture = null)
    {
        if (picture != null )
        {
            characterPortrait.setImage(Resources.Load<Sprite>(picture));
            characterPortrait.setVisible(true);
        }

         
        
        if (textos.Length <= 0 || dialog != null)
        {
            return;
        }

        dialog = textos;
        currentDialog = 0;

        text.setText(dialog[currentDialog]);

        shadow.setVisible(true);
        text.setVisible(true);
    }

    public static bool isTalking()
    {
        return dialog != null;
    }
	
	public static void destroy()
	{
		if (mInitialized) 
		{
			mInitialized = false;

            shadow.destroy();
            text.destroy();
            characterPortrait.destroy();

        }
	}
}

 