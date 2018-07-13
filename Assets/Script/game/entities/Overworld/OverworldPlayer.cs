using UnityEngine;
using System.Collections;

public class COverWorldPlayer : CAnimatedSprite
{
    private const float SPEED = 400.0f;
    private const int HEIGHT = 32;
    private const int WIDTH = 32;

    private const int STATE_STAND = 0;
    private const int STATE_WALKING = 1;
    private const int X_OFFSET_BOUNDING_BOX = 4 * 5;
    private const int Y_OFFSET_BOUNDING_BOX = 4 * 5;
    private float mOldY;

    private CSprite mRect;

    public COverWorldPlayer()
    {
        setFrames(Resources.LoadAll<Sprite>("Sprites/PlayerWalking"));
        setName("Overworld player");
        setSortingLayerName("Personajes");

        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.STOP);
        setScale(5f);

        setRegistration(CSprite.REG_TOP_LEFT);
        setWidth(WIDTH*5);
        setHeight(HEIGHT*5);
        setState(STATE_STAND);

        setXOffsetBoundingBox(X_OFFSET_BOUNDING_BOX);
        setYOffsetBoundingBox(Y_OFFSET_BOUNDING_BOX);

        mRect = new CSprite();
        mRect.setImage(Resources.Load<Sprite>("Sprites/ui/pixel"));
        mRect.setSortingLayerName("Personajes");
        mRect.setSortingOrder(20);
        mRect.setAlpha(0.5f);
        mRect.setName("player_debug_rect");
    }
    private void setOldYPosition()
    {
        mOldY = getY();
    }
    override public void update()
    {
        //Se considera la posicion anterior del objeto.
        setOldYPosition();

        base.update();

        if (getState() == STATE_STAND)
        {
            // En stand no deberia pasar nunca que quede metido en una pared.
            // Si estamos en una pared, corregirnos. 
            if (isWallLeft(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared.
                setX(((mLeftX + 1) * CTileMap.TILE_WIDTH) - X_OFFSET_BOUNDING_BOX);
            }
            if (isWallRight(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared.
                setX((((mRightX) * CTileMap.TILE_WIDTH) - getWidth()) + X_OFFSET_BOUNDING_BOX);
            }
            if (isRoof(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared.
                setX(((mUpY + 1) * CTileMap.TILE_HEIGHT) + Y_OFFSET_BOUNDING_BOX);
            }
            if (isFloor(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared.
                setX((((mDownY) * CTileMap.TILE_HEIGHT) - getHeight()) + Y_OFFSET_BOUNDING_BOX);
            }

            if (CKeyboard.pressed(CKeyboard.LEFT) && !isWallLeft(getX() - 1, getY()))
            {
                setState(STATE_WALKING);
                
                return;
            }
            if (CKeyboard.pressed(CKeyboard.RIGHT) && !isWallRight(getX() + 1, getY()))
            {
                setState(STATE_WALKING);
                return;
            }
            if (CKeyboard.pressed(CKeyboard.UP) && !isRoof(getX(), getY() - 1))
            {
                setState(STATE_WALKING);
                return;
            }
            if (CKeyboard.pressed(CKeyboard.DOWN) && !isFloor(getX(), getY() + 1))
            {
                setState(STATE_WALKING);
                return;
            }

        }
        else if (getState() == STATE_WALKING)
        {
            if (isWallLeft(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared y que no la traspase del lado izquierdo.
                setX(((mLeftX + 1) * CTileMap.TILE_WIDTH) - X_OFFSET_BOUNDING_BOX);
            }
            if (isWallRight(getX(), getY()))
            {
                // Reposicionar el personaje contra la pared y que no la traspase del lado derecho.
                setX((((mRightX) * CTileMap.TILE_WIDTH) - getWidth()) + X_OFFSET_BOUNDING_BOX);
            }
            if (isRoof(getX(),getY()))
            {
                //Reposicionar el personaje contra la pared que no la traspase del lado superior.
                setY(((mUpY + 1) * CTileMap.TILE_HEIGHT) - Y_OFFSET_BOUNDING_BOX);
            }
            if (isFloor(getX(), getY()))
            {
                //Reposicionar el personaje contra la pared que no la traspase del lado inferior.
                setY(((mDownY) * CTileMap.TILE_HEIGHT - getHeight()) -   Y_OFFSET_BOUNDING_BOX);
            }
            if (!(CKeyboard.pressed(CKeyboard.LEFT) || CKeyboard.pressed(CKeyboard.RIGHT) || CKeyboard.pressed(CKeyboard.UP) || CKeyboard.pressed(CKeyboard.DOWN)))
            {
                setState(STATE_STAND);
                return;
            }
            moveHorizontal();
            moveVertical();
        }
        // MOSTRAR TODA EL AREA DEL DIBUJO.
        
    }

    override public void render()
    {
        base.render();
        
        /*mRect.setXY(getX(), getY());
        mRect.setScaleX(WIDTH*5);
        mRect.setScaleY(HEIGHT*5);
        mRect.update();
        mRect.render();*/
    }

    override public void destroy()
    {
        base.destroy();
        mRect.destroy();
        mRect = null;
    }

    /*private bool isNorth(float aX, float aY)
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
    }*/
    
    public override void setState(int aState)
    {
        base.setState(aState);
        
        if (getState() == STATE_STAND)
        {
            initAnimation(1, 12, 12, true);
            stopMove();
        }
        if (getState() == STATE_WALKING)
        {
            initAnimation(13, 36, 12, true);
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
                if (isWallLeft(getX(), getY()))
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
                    setFlip(true);
                }
            }
            else if (CKeyboard.pressed(CKeyboard.RIGHT))
            {
                // Chequear pared a la derecha.
                // Si hay pared a la derecha vamos a stand.
                if (isWallRight(getX(), getY()))
                {
                    // Reposicionar el personaje contra la pared.
                    setX(((mRightX) * CTileMap.TILE_WIDTH) - getWidth());

                }
                else
                {
                    // No hay pared, se puede mover.
                    setVelX(400);
                    setFlip(false);
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
                if (isRoof(getX(), getY()))
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
                if (isFloor(getX(), getY()))
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
