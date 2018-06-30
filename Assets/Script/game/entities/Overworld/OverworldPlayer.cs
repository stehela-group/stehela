using UnityEngine;
using System.Collections;

public class COverWorldPlayer : CAnimatedSprite
{
    private const float SPEED = 400.0f;
    private const int HEIGHT = 230;
    private const int WIDTH = 150;
    // Variables auxiliares que se cargan cuando llamamos a checkPoints().
    private int mTileTopLeft;
    private int mTileTopRight;
    private int mTileDownLeft;
    private int mTileDownRight;
    private int mLeftX;
    private int mRightX;
    private int mUpY;
    private int mDownY;

    private const int STATE_STAND = 0;
    private const int STATE_WALKING = 1;

    private CSprite mRect;

    public COverWorldPlayer()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/worldPlayer"));
        setName("Overworld player");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);

        setRegistration(CSprite.REG_TOP_LEFT);
        setWidth(WIDTH);
        setHeight(HEIGHT);
        setState(STATE_STAND);

        mRect = new CSprite();
        mRect.setImage(Resources.Load<Sprite>("Sprites/ui/pixel"));
        mRect.setSortingLayerName("Personajes");
        mRect.setSortingOrder(20);
        mRect.setAlpha(0.5f);
        mRect.setName("player_debug_rect");
        mRect.setParentObject(this.getTransform());
    }

    override public void update()
    {

        base.update();

        if (getState() == STATE_STAND)
        {
            if (CKeyboard.pressed(CKeyboard.LEFT) && !isLeft(getX() - 1, getY()))
            {
                setState(STATE_WALKING);
                return;
            }
            if (CKeyboard.pressed(CKeyboard.RIGHT) && !isRight(getX() + 1, getY()))
            {
                setState(STATE_WALKING);
                return;
            }
            if (CKeyboard.pressed(CKeyboard.UP) && !isNorth(getX(), getY() - 1))
            {
                setState(STATE_WALKING);
                return;
            }
            if (CKeyboard.pressed(CKeyboard.DOWN) && !isSouth(getX(), getY() + 1))
            {
                setState(STATE_WALKING);
                return;
            }

        }
        else if (getState() == STATE_WALKING)
        {
            if (!(CKeyboard.pressed(CKeyboard.LEFT) || CKeyboard.pressed(CKeyboard.RIGHT) || CKeyboard.pressed(CKeyboard.UP) || CKeyboard.pressed(CKeyboard.DOWN)))
            {
                setState(STATE_STAND);
                return;
            }
            moveHorizontal();
            moveVertical();
        }
        // MOSTRAR TODA EL AREA DEL DIBUJO.
        mRect.setXY(getX(), getY());
        mRect.setScaleX(WIDTH);
        mRect.setScaleY(HEIGHT);
        mRect.update();
    }

    override public void render()
    {
        base.render();
        mRect.render();
    }

    override public void destroy()
    {
        base.destroy();
        mRect.destroy();
        mRect = null;
    }

    private bool isNorth(float aX, float aY)
    {
        //Cargar las variables
        checkbounds(aX, aY);
        if (mTileTopLeft == 1 || mTileTopRight == 1)

            return true;
        else
            return false;
    }

    private bool isSouth (float aX, float aY)
    {
        //Cargar las variables
        checkbounds(aX, aY);

        if (mTileDownLeft == 1 || mTileDownRight == 1)
            return true;
        else
            return false;
    }

    private bool isRight (float aX, float aY)
    {
        //Cargar las Variables
        checkbounds(aX, aY);

        if (mTileDownRight == 1 || mTileTopRight == 1)
            return true;
        else
            return false;
    }

    private bool isLeft(float aX, float aY)
    {
        //Cargar las Variables
        checkbounds(aX, aY);

        if (mTileTopLeft == 1 || mTileDownLeft == 1)
            return true;
        else
            return false;
    }

    private void checkbounds(float aX, float aY)
    {
        int x = (int)aX;
        int y = (int)aY;

        //Columna en la parte izquierda del player
        mLeftX = x / CTileMap.TILE_WIDTH;
        //Columna en la parte derecha del player
        mRightX = (x + getWidth() - 1) / CTileMap.TILE_WIDTH;
        //Fila en la parte superior del Player
        mUpY = y / CTileMap.TILE_HEIGHT;
        //Fila en la parte inferior del Player
        mDownY = (y + getHeight() - 1) / CTileMap.TILE_HEIGHT;

        CTileMap map = CGame.inst().getMap();

        mTileTopLeft = map.getTileIndex(mLeftX, mUpY);
        mTileTopRight = map.getTileIndex(mRightX, mUpY);
        mTileDownLeft = map.getTileIndex(mLeftX, mDownY);
        mTileDownRight = map.getTileIndex(mRightX, mDownY);
    }
    
    public override void setState(int aState)
    {
        base.setState(aState);
        
        if (getState() == STATE_STAND)
        {
            stopMove();
            gotoAndStop(1);
        }
        if (getState() == STATE_WALKING)
        {
            return;
        }
        if (getState() == STATE_WALKING)
        {
            return;
        }

    }
    private void stopMove()
    {
        setVelXY(0, 0);
    }
    private void moveHorizontal()
    {
        if (!(CKeyboard.pressed(CKeyboard.LEFT) || CKeyboard.pressed(CKeyboard.RIGHT )))
        {
            setVelX(0);
            return;
        }

        else
        {
            if (CKeyboard.pressed(CKeyboard.LEFT))
            {
                // Chequear pared a la izquierda.
                // Si hay pared a la izquierda vamos a stand.
                if (isLeft(getX(), getY()))
                {
                    // Reposicionar el personaje contra la pared.
                    //setX((((int) getX ()/CTileMap.TILE_WIDTH)+1)*CTileMap.TILE_WIDTH);
                    setX((mLeftX + 1) * CTileMap.TILE_WIDTH);

                    // Carlos version.
                    //setX (getX()+CTileMap.TILE_WIDTH/(getWidth()-1));

                }
                else
                {
                    // No hay pared, se puede mover.
                    setVelX(-400);
                    //setFlip(true);
                }
            }
            else if (CKeyboard.pressed(CKeyboard.RIGHT))
            {
                // Chequear pared a la derecha.
                // Si hay pared a la derecha vamos a stand.
                if (isRight(getX(), getY()))
                {
                    // Reposicionar el personaje contra la pared.
                    setX(((mRightX) * CTileMap.TILE_WIDTH) - getWidth());

                }
                else
                {
                    // No hay pared, se puede mover.
                    setVelX(400);
                    //setFlip(false);
                }
            }
        }
    }

    private void moveVertical()
    {
        if (!( CKeyboard.pressed(CKeyboard.UP) || CKeyboard.pressed(CKeyboard.DOWN) ))
        {
            setVelY(0);
            return;
        }
        else
        {
            if (CKeyboard.pressed(CKeyboard.UP))
            {
                // Chequear pared a la izquierda.
                // Si hay pared a la izquierda vamos a stand.
                if (isNorth(getX(), getY()))
                {
                    // Reposicionar el personaje contra la pared.
                    //setX((((int) getX ()/CTileMap.TILE_WIDTH)+1)*CTileMap.TILE_WIDTH);
                    setY((mUpY + 1) * CTileMap.TILE_HEIGHT);

                    // Carlos version.
                    //setX (getX()+CTileMap.TILE_WIDTH/(getWidth()-1));

                }
                else
                {
                    // No hay pared, se puede mover.
                    setVelY(-400);
                    //setFlip(true);
                }
            }
            else if (CKeyboard.pressed(CKeyboard.DOWN))
            {
                // Chequear pared a la derecha.
                // Si hay pared a la derecha vamos a stand.
                if (isSouth(getX(), getY()))
                {
                    // Reposicionar el personaje contra la pared.
                    setY(((mDownY) * CTileMap.TILE_HEIGHT) - getHeight());

                }
                else
                {
                    // No hay pared, se puede mover.
                    setVelY(400);
                    //setFlip(false);
                }
            }
        }
    }
}
