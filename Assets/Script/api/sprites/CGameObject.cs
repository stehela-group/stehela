﻿using UnityEngine;
using System.Collections;

public class CGameObject 
{
	private CVector mPos;
	private CVector mVel;
	private CVector mAccel;
	
	private bool mIsDead = false;
	
	private int mState = 0;
	private float mTimeState = 0.0f;
	
	private string mName;

	private int mRadius = 100;

	private int mType;

	private int mWidth = 0;
	private int mHeight = 0;

    private float mFriction = 1.0f;


    public const int NONE = 0;
    public const int STOP = 1;
    public const int WRAP = 2;
    public const int BOUNCE = 3;
    public const int DIE = 4;

    private int mMinX = 0;
    private int mMinY = 0;
    private int mMaxX = 0;
    private int mMaxY = 0;

    // Comportamiento de bordes del objeto
    public int mBoundAction = NONE;
    // Velocidad Maxima.
    private float mMaxSpeed = CMath.INFINITY;
    // Max. acceleration.
    private float mMaxAccel = CMath.INFINITY;

    // Platform game -------------------------------------------------------------
    // Variables auxiliares que se cargan cuando llamamos a checkPoints().
    protected bool mTileTopLeft; // Si es caminables (true) o no (false).
    protected bool mTileTopRight;
    protected bool mTileDownLeft;
    protected bool mTileDownRight;
    protected int mLeftX; // Columna de la izquierda.
    protected int mRightX;
    protected int mUpY;
    protected int mDownY;

    private int mXOffsetBoundingBox = 0;
    private int mYOffsetBoundingBox = 0;
    //-----------------------------------------------------------------------------

    public CGameObject()
	{
		mPos = new CVector ();
		mVel = new CVector ();
		mAccel = new CVector ();
	}
	
	public void setX(float aX)
	{
		mPos.x = aX;
	}
	
	public void setY(float aY)
	{
		mPos.y = aY;
	}
	
	public void setZ(float aZ)
	{
		mPos.z = aZ;
	}

	public void setXY(float aX, float aY)
	{
		mPos.x = aX;
		mPos.y = aY;
	}
	
	public float getX()
	{
		return mPos.x;
	}
	
	public float getY()
	{
		return mPos.y;
	}
	
	public float getZ()
	{
		return mPos.z;
	}
	
	public void setVelX(float aVelX)
	{
		mVel.x = aVelX;
	}
	
	public void setVelY(float aVelY)
	{
		mVel.y = aVelY;
	}

	public void setVelXY(float aVelX, float aVelY)
	{
		mVel.x = aVelX;
		mVel.y = aVelY;
	}

	public void setVelZ(float aVelZ)
	{
		mVel.z = aVelZ;
	}
	
	public float getVelX()
	{
		return mVel.x;
	}
	
	public float getVelY()
	{
		return mVel.y;
	}
	
	public float getVelZ()
	{
		return mVel.z;
	}
	
	public void setAccelX(float aAccelX)
	{
		mAccel.x = aAccelX;
	}
	
	public void setAccelY(float aAccelY)
	{
		mAccel.y = aAccelY;
	}
	
	public void setAccelZ(float aAccelZ)
	{
		mAccel.z = aAccelZ;
	}
	
	public float getAccelX()
	{
		return mAccel.x;
	}
	
	public float getAccelY()
	{
		return mAccel.y;
	}
	
	public float getAccelZ()
	{
		return mAccel.z;
	}
	
	virtual public void update()
	{
		mTimeState = mTimeState + Time.deltaTime;

        // Truncate acceleration.
        mAccel.truncate(mMaxAccel);

        mVel = mVel + mAccel * Time.deltaTime;

        // Apply friction.
        mVel.mul(mFriction);

        // Truncate speed.  
        mVel.truncate(mMaxSpeed);

        mPos = mPos + mVel * Time.deltaTime;

        checkBounds();
    }
	
	virtual public void render()
	{
	}
	
	virtual public void destroy()
	{
		mPos.destroy ();
		mPos = null;
		mVel.destroy ();
		mVel = null;
		mAccel.destroy ();
		mAccel = null;
	}

	virtual public void setState(int aState)
	{
		mState = aState;
		mTimeState = 0.0f;
	}

	public int getState()
	{
		return mState;
	}

	public float getTimeState()
	{
		return mTimeState;
	}

	public void setDead(bool aIsDead)
	{
		mIsDead = aIsDead;
	}

	public bool isDead()
	{
		return mIsDead;
	}

	public void setRadius(int aRadius)
	{
		mRadius = aRadius;
	}

	public int getRadius()
	{
		return mRadius;
	}

	public void setType(int aType)
	{
		mType = aType;
	}

	public int getType()
	{
		return mType;
	}

	virtual public void setName(string aName)
	{
		mName = aName;
	}

	virtual public string getName()
	{
		return mName;
	}

	public void setWidth(int aWidth)
	{
		mWidth = aWidth;
	}

	public int getWidth()
	{
		return mWidth;
	}

	public void setHeight(int aHeight)
	{
		mHeight = aHeight;
	}
	
	public int getHeight()
	{
		return mHeight;
	}

	public bool collides(CGameObject aGameObject)
	{
		if (CMath.dist (getX (), getY (), aGameObject.getX (), aGameObject.getY ()) < (getRadius () + aGameObject.getRadius ()))
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
    public void setFriction(float aFriction)
    {
        mFriction = aFriction;
    }

    public float getFriction()
    {
        return mFriction;
    }

    // COMPORTAMIENTO QUE ALCANZA EL OBJETO AL TOCAR SUS PROPIOS BORDES DEFINIDOS.
    public void setBoundAction(int aBoundAction)
    {
        mBoundAction = aBoundAction;
    }

    // El máximo y mínimo que puede llegar el objeto
    public void setBounds(int aMinX, int aMinY, int aMaxX, int aMaxY)
    {
        mMinX = aMinX;
        mMinY = aMinY;
        mMaxX = aMaxX;
        mMaxY = aMaxY;
    }

    // Sirve para chequear los bordes.
    public void checkBounds()
    {
        // Si es none no se hace nada.
        if (mBoundAction == NONE)
        {
            return;
        }

        bool left = getX() < mMinX;
        bool right = getX() > mMaxX;
        bool up = getY() < mMinY;
        bool down = getY() > mMaxY;

        // Si no toca los bordes no hace nada (no pasa nada).

        //Si no se va ni para izquierda o para derecha o para abajo o para arriba
        if (!(left || right || up || down))
        {
            return;
        }

        // WRAP: el objeto desaparece y aparece del lado contrario.
        if (mBoundAction == WRAP)
        {
            if (left)  //si se va para la izquierda
            {
                setX(mMaxX);   // aparece en la derecha
            }
            if (right)   //si se va para la derecha
            {
                setX(mMinX);  // aparece en la izquierda
            }
            if (up)   //si se va por arribe
            {
                setY(mMaxY);   // aparece abajo
            }
            if (down)   //si se va por abajo
            {
                setY(mMinY);   //aprarece arriba
            }
        }
        else
        {
            if (left)
            {
                setX(mMinX);
            }
            if (right)
            {
                setX(mMaxX);
            }
            if (up)
            {
                setY(mMinY);
            }
            if (down)
            {
                setY(mMaxY);
            }
        }

        //En el caso que sea STOP o DIE el objeto deja de moverse
        if (mBoundAction == STOP || mBoundAction == DIE)
        {
            setVelXY(0, 0);
        }
        //En el caso bounce el objeto rebota del borde
        else if (mBoundAction == BOUNCE)
        {
            if (right || left)
            {
                setVelX(getVelX() * -1);
            }
            if (up || down)
            {
                setVelY(getVelY() * -1);
            }
        }
        //El objeto muere
        if (mBoundAction == DIE)
        {
            mIsDead = true;
            return;
        }
    }

    public void stopMove()
    {
        setVelXY(0, 0);
        setAccelX(0);
        setAccelY(0);
    }

    public void setXOffsetBoundingBox(int aXOffsetBoundingBox)
    {
        mXOffsetBoundingBox = aXOffsetBoundingBox;
    }

    public void setYOffsetBoundingBox(int aYOffsetBoundingBox)
    {
        mYOffsetBoundingBox = aYOffsetBoundingBox;
    }

    //--------------------------------------------------------------
    // PLATFORM GAME 
    //--------------------------------------------------------------

    // Devuelve true si hay un techo arriba del personaje.
    public bool isRoof(float aX, float aY)
    {
        // se cargan las variables.
        checkPoints(aX, aY);

        if (!mTileTopLeft || !mTileTopRight)
            return true;
        else
            return false;
    }
    // Devuelve true si hay un piso abajo del personaje.
    public bool isFloor(float aX, float aY)
    {
        // Esto carga las variables...
        checkPoints(aX, aY);

        if (!mTileDownLeft || !mTileDownRight)
            return true;
        else
            return false;
    }

    // Devuelve true si hay una pared en el lado izquierdo del personaje.
    public bool isWallLeft(float aX, float aY)
    {
        // Esto carga las variables...
        checkPoints(aX, aY);

        if (!mTileTopLeft || !mTileDownLeft)
            return true;
        else
            return false;
    }

    // Devuelve true si hay una pared en el lado derecho del personaje.
    public bool isWallRight(float aX, float aY)
    {
        // Esto carga las variables...
        checkPoints(aX, aY);

        if (!mTileTopRight || !mTileDownRight)
            return true;
        else
            return false;
    }
    public void checkPoints(float aX, float aY)
    {
        int x = (int)aX;
        int y = (int)aY;

        // Columna del lado izquierdo del personaje.
        mLeftX = (x + mXOffsetBoundingBox) / CTileMap.TILE_WIDTH;
        // Columna del lado derecho del personaje. -1 porque es el pixel de adentro. x+w es la coordenada del pixel de afuera.
        mRightX = (x + getWidth() - 1 - mXOffsetBoundingBox) / CTileMap.TILE_WIDTH;
        // Fila de arriba del personaje.
        mUpY = (y + mYOffsetBoundingBox) / CTileMap.TILE_HEIGHT;
        // Fila de los pies del personaje.
        mDownY = (y + getHeight() - 1) / CTileMap.TILE_HEIGHT;

        CTileMap map = CGame.inst().getMap();

        mTileTopLeft = map.getTile(mLeftX, mUpY).isWalkable();
        mTileTopRight = map.getTile(mRightX, mUpY).isWalkable();
        mTileDownLeft = map.getTile(mLeftX, mDownY).isWalkable();
        mTileDownRight = map.getTile(mRightX, mDownY).isWalkable();

        //Debug.Log ("Esquina superior izquierda hay un tile: " + mTileTopLeft);
        //Debug.Log ("Esquina superior derecha hay un tile: " + mTileTopRight);
        //Debug.Log ("Esquina inferior izquierda hay un tile: " + mTileDownLeft);
        //Debug.Log ("Esquina inferior derecha hay un tile: " + mTileDownRight);
    }
}