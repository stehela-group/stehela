﻿using UnityEngine;
using System.Collections;

public class CSprite : CGameObject 
{
	private GameObject mSprite;
	private SpriteRenderer mSpriteRenderer;

	// Caching of mSprite.transform.
	private Transform mTransform;

	private bool mFlip = false;
	private float mRotation = 0.0f;
	private bool mIsRotatingSprite = false;

	public const int REG_CENTER = 0;
	public const int REG_TOP_LEFT = 1;
	private int mRegistration = REG_CENTER;

	public static int PLAYER_WIDTH = 64;
	public static int PLAYER_HEIGHT = 74;

	public CSprite()
	{
		mSprite = new GameObject ();
		mSpriteRenderer = mSprite.AddComponent<SpriteRenderer> ();

		mTransform = mSprite.transform;

		setWidth (PLAYER_WIDTH);
		setHeight (PLAYER_HEIGHT);
	}

	override public void update()
	{
		base.update ();


	}

	override public void render()
	{
		base.render ();

		int offsetX = 0;

		if (mRegistration == REG_TOP_LEFT) 
		{
			if (mFlip) 
			{
				offsetX = getWidth ();
			}
		}

		Vector3 pos = new Vector3 (getX () + offsetX, getY () * -1, 0.0f);
		mTransform.position = pos;

		if (!mIsRotatingSprite) 
		{
			if (mFlip) 
			{
				mTransform.rotation = Quaternion.Euler (new Vector3 (0, 180, mRotation));
			} 
			else 
			{
				mTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, mRotation));
			}
		} 
		else 
		{
			mTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, mRotation));
		}
	}

	override public void destroy()
	{
		base.destroy ();
		Object.Destroy (mSprite);
	}

	public void setImage(Sprite aSprite)
	{
		mSpriteRenderer.sprite = aSprite;
		this.setHeight((int) aSprite.rect.height);
		this.setWidth((int) aSprite.rect.width);
	}

	public void setFlip(bool aFlip)
	{
		mFlip = aFlip;
	}

	public bool getFlip()
	{
		return mFlip;
	}

	public void setRotation(float aRotation)
	{
		mIsRotatingSprite = true;
		mRotation = aRotation;
		mRotation = CMath.clampDeg (mRotation);
	}

	public float getRotation()
	{
		return mRotation;
	}

	override public void setName(string aName)
	{
		base.setName (aName);
		mSprite.name = aName;
	}

	public void setSortingLayerName(string aSortingLayerName)
	{
		mSpriteRenderer.sortingLayerName = aSortingLayerName;
	}

	public string getSortingLayerName()
	{
		return mSpriteRenderer.sortingLayerName;
	}

	public void setSortingOrder(int aSortingOrder)
	{
		mSpriteRenderer.sortingOrder = aSortingOrder;
	}

	public int getSortingOrder()
	{
		return mSpriteRenderer.sortingOrder;
	}

	public void setColor(Color aColor)
	{
		mSpriteRenderer.material.color = aColor;
	}

	public Color getColor()
	{
		return mSpriteRenderer.material.color;
	}

	public void setAlpha(float aAlpha)
	{
		Color color = mSpriteRenderer.material.color;
		mSpriteRenderer.material.color = new Color (color.r, color.g, color.b, aAlpha);
	}

	public float getAlpha()
	{
		Color color = mSpriteRenderer.material.color;
		return color.a;
	}

	virtual public void setVisible(bool aIsVisible)
	{
		mSpriteRenderer.enabled = aIsVisible;
	}

	public bool isVisible()
	{
		return mSpriteRenderer.enabled;
	}

	public void setScale(float aScale)
	{
		mSprite.transform.localScale = new Vector3 (aScale, aScale, 0.0f);
	}

    public void setScaleX(float aScaleX)
    {
        mSprite.transform.localScale = new Vector3(aScaleX, mSprite.transform.localScale.y, 0.0f);
    }

    public void setScaleY(float aScaleY)
    {
        mSprite.transform.localScale = new Vector3(mSprite.transform.localScale.x, aScaleY, 0.0f);
    }

    public void setRegistration(int aRegistration)
	{
		mRegistration = aRegistration;
	}

	public int getRegistration()
	{
		return mRegistration;
	}

	public void setParentObject(Transform transform)
	{
		this.mSprite.transform.SetParent(transform);
	}

	public Transform getTransform()
	{
		return this.mSprite.transform;
	}
}
