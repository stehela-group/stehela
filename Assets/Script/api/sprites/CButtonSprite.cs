using UnityEngine;
using System.Collections;

public class CButtonSprite : CAnimatedSprite
{
	public bool mIsMouseOver = false;
	protected CText buttonText;

	public CButtonSprite(string buttonText = null)
	{
		this.setName("Button - " + buttonText);
		this.buttonText = new CText(buttonText);

		this.setFrames(Resources.LoadAll<Sprite>("Sprites/ui"));
		this.gotoAndStop(1);
		this.setWidth(190);
		this.setHeight(50);
		this.setSortingLayerName("UI");

		Debug.Log("Boton creado wn");
	}

	public override void destroy()
	{
		base.destroy ();
	}

	public override void update()
	{
		base.update ();

		float scale = 1.0f;
		int frame = 1;

		Vector3 mousePos = CMouse.getPos ();
		mIsMouseOver = CMath.pointInRect (mousePos.x, mousePos.y, getX () - getWidth () / 2, getY () - getHeight () / 2, getWidth (), getHeight ());

		if (CMouse.pressed ()) 
		{
			if (mIsMouseOver) 
			{
				scale = 0.9f;
				frame = 2;
			}
		} 
		else 
		{
			if (mIsMouseOver)
			{
				scale = 1.1f;
				frame = 1;
			}
			else
			{
				scale = 1.0f;
				frame = 1;
			}
		}

		setScale (scale);
		gotoAndStop (frame);

		this.buttonText.setXY((this.getX() + this.getWidth()) / 2, (this.getY() + this.getHeight()) / 2);
	}

	public override void render()
	{
		base.render ();
		this.buttonText.render();
	}

	public bool isMouseOver()
	{
		return mIsMouseOver;
	}

	public bool pressed()
	{
		return (CMouse.pressed () && mIsMouseOver);
	}

	public bool clicked()
	{
		Vector3 mousePos = CMouse.getPos ();

		if (CMouse.release ()) 
		{
			if (CMath.pointInRect (mousePos.x, mousePos.y, getX () - getWidth () / 2, getY () - getHeight () / 2, getWidth (), getHeight ()))
			{
				return true;
			}
		}

		return false;
	}
}