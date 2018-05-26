using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : CSprite 
{
    public const int IDLE = 0;
    public const int RECEIVING_DAMAGE = 1;
    public const int ATTACKING = 2;
    public const int DEAD = 3;

    public int maxHealth = 0;
    public int currentHealth = 0;

    public string name;
    protected List<Skill> skills = new List<Skill>();
    // Use this for initialization
    public void init (int characterName, int characterHealth)
    {
        //Nombre de la entidad
        name = characterName;
        EntityHealth(characterHealth);
        
        


        //List<Skill> skills: Lista de skills de la entidad

        //Estados:
        //IDLE: Estado mientras la entidad está en espera 
        //RECEIVING_DAMAGE: Estado de la entidad al ser atacada
        //ATTACKING: Estado de la entidad al atacar

    }
    public void EntityHealth(int health)
    {
        //vida maxima del personaje
        maxHealth = health;
        //vida actual del personaje
        currentHealth = health;
    }


// Update is called once per frame
    override public void update ()
    {
        base.update();
		switch (this.getState())
        {
            case (BattleEntity.IDLE):
                break;
            case (BattleEntity.RECEIVING_DAMAGE):
                break;
            case (BattleEntity.ATTACKING):
                break;
            case (BattleEntity.DEAD):
                break;
        }
	}
    override public void render ()
    {
        base.render();
        switch (this.getState())
        {
            case (BattleEntity.IDLE):
                break;
            case (BattleEntity.RECEIVING_DAMAGE):
                break;
            case (BattleEntity.ATTACKING):
                break;
            case (BattleEntity.DEAD):
                break;
        }
    }
    override public void destroy()
    {
        base.destroy();
    }
}