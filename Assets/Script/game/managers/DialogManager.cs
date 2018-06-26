using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogManager 
{
    static private bool mInitialized = false;
    static private CSprite background;
    static private CText text;
    static private string[] dialog;
    static private int currentDialog;

    public DialogManager()
	{
		throw new UnityException ("Error in DialogManager(). You're not supposed to instantiate this class.");
	}
	
	public static void init()
	{
		if (mInitialized) 
		{
			return;
		}
		mInitialized = true;

        background = new CSprite();
        //Esta tambien el shadow solo que llena toda la pantalla
        background.setImage(Resources.Load<Sprite>("Sprites/dialogShadow/shadow3"));
        background.setName("Dialog - Background");
        background.setSortingLayerName("UI");
        background.setXY(0, 0);
        background.setWidth(CGameConstants.SCREEN_WIDTH);
        background.setVisible(false);

        text = new CText("");
        text.setXY(0, 0);
        text.setFontSize(300f);
        text.setVisible(false);
        text.setWidth(CGameConstants.SCREEN_WIDTH);
	}

	public static void update()
	{
        background.update();
        text.update();

        if(dialog != null)
        {
            if(CKeyboard.firstPress(CKeyboard.ENTER) && dialog.Length > 0 && currentDialog < dialog.Length - 1)
            {
                currentDialog++;
                text.setText(dialog[currentDialog]);
                text.setXY(500, 800);
            }
            else if(CKeyboard.firstPress(CKeyboard.ENTER) && currentDialog == dialog.Length - 1)
            {
                dialog = null;
                text.setText("");
                background.setVisible(false);
                text.setVisible(false);
                currentDialog = 0;
            }
        }
	}

    public static void render()
    {
        background.render();
        text.render();
    }

    public static void startDialog(string[] textos)
    {
        text.setXY(500, 800);
        if (textos.Length <= 0 || dialog != null)
        {
            return;
        }

        dialog = textos;
        currentDialog = 0;

        text.setText(dialog[currentDialog]);

        background.setVisible(true);
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

            background.destroy();
            text.destroy();
		}
	}
}

 