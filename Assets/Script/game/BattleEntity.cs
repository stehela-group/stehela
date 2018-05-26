using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleEntity : CSprite 
{
    public const int IDLE = 0;
    public const int RECEIVING_DAMAGE = 1;
    public const int ATTACKING = 2;
    public const int DEAD = 3;

    protected int maxHealth = 0;
    protected int currentHealth = 0;

    protected string name;
    protected List<Skill> skills = new List<Skill>();
    // Use this for initialization
    public void init (string characterName, int characterMaxHealth)
    {
        //Nombre de la entidad
        this.name = characterName;
        this.maxHealth = characterMaxHealth;
        this.setHealth(characterMaxHealth);
        
        //List<Skill> skills: Lista de skills de la entidad
        //Estados:
        //IDLE: Estado mientras la entidad está en espera 
        //RECEIVING_DAMAGE: Estado de la entidad al ser atacada
        //ATTACKING: Estado de la entidad al atacar

    }

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

    /// <summary>
    /// Setea la vida de la entidad
    /// </summary>
    /// <param name="health"> Nueva vida de la entidad </param>
    public void setHealth(int health)
    {
        //this.currentHealth = health > this.maxHealth ? this.maxHealth : health;
    }
}