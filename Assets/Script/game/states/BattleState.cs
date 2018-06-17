using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
class BattleState : CGameState
{
	const int SELECTING_ACTIONS = 0;
	const int PERFORMING_ACTIONS = 1;
	const int PLAYER_WON = 2;
	const int PLAYER_LOST = 3;

    private CBackground mBackground;

    //private turnActionManager mTurnActionManager;

    // Equipo del jugador
    protected List<BattleEntity> playerParty = new List<BattleEntity>();

	// Equipo Enemigo
	protected List<BattleEntity> enemyParty = new List<BattleEntity>();

    // variable para chequear que todos los enemigos esten muertos
    protected int deadEnemiesCount = 0;

	// Acciones a realizar este turno
	protected List<Action> selectedActions = new List<Action>();

    // Acciones a realizar por el enemigo
    protected Dictionary<BattleEntity, Skill> enemySelectedActions = new Dictionary<BattleEntity, Skill>();

	// Entidad del jugador seleccionada en este momento
	protected BattleEntity selectedBattleEntity = null;

    // Entidad de skill seleccionada en este momento
    protected Skill selectedSkill = null; 

    protected Dictionary<CButtonSprite, Skill> skillButtons = new Dictionary<CButtonSprite, Skill>();

    protected Dictionary<CButtonSprite, BattleEntity> playerPartyButtons = new Dictionary<CButtonSprite, BattleEntity>();

    protected Dictionary<CButtonSprite, BattleEntity> enemyPartyButtons = new Dictionary<CButtonSprite, BattleEntity>();


    public BattleState()
    {
		
        this.playerParty.Add(new BattlePlayer());
		this.playerParty.Add(new Companion1());

		
        this.enemyParty.Add(new PracticeBoss());
        this.enemyParty.Add(new Enemy2());

        //mTurnActionManager = new turnActionManager();
    }

    override public void init()
    {

        foreach (var entity in this.playerParty)
        {
            CButtonSprite playerButton = new CButtonSprite(entity.getName());
            playerButton.setXY( 200, (playerButton.getHeight() * this.playerPartyButtons.Count) + 50);
            //playerButton.setSortingLayerName("Game");
            this.playerPartyButtons.Add(playerButton, entity);
        }

		// Agregamos botones con los enemigos de la entidad
		foreach (var entity in this.enemyParty)
		{
			CButtonSprite enemyButton = new CButtonSprite(entity.getName());

			enemyButton.setXY(CGameConstants.SCREEN_WIDTH - (enemyButton.getWidth() * 1.1f),
								CGameConstants.SCREEN_HEIGHT - (enemyButton.getHeight() * (this.enemyParty.Count - this.enemyPartyButtons.Count)));
            //enemyButton.setSortingLayerName("Game");
			this.enemyPartyButtons.Add(enemyButton, entity);
		}


        mBackground = new CBackground();
        mBackground.setXY(0, 0);
        mBackground.setSortingLayerName("Background");
		this.setState(BattleState.SELECTING_ACTIONS);
    }

    override public void update()
    {

        switch (this.getState())
        {
            case BattleState.SELECTING_ACTIONS:
				// Botones de seleccion de personaje
				foreach (var entry in this.playerPartyButtons)
				{
					// entry.Key = CButtonSprite
					// entry.Value = BattleEntity
					entry.Key.update();

					//Si se hizo click en el botón y la entidad es diferente a la actual
					if (entry.Key.clicked() && this.selectedBattleEntity != entry.Value)
					{
						this.selectedBattleEntity = entry.Value;

						// Deselecciono la habilidad seleccionada
						this.selectedSkill = null;

						// Destruimos botones antiguos de skills
						this.clearSkillButtons();

						// Agregamos botones con los skills de la entidad actual
						foreach (var skill in this.selectedBattleEntity.getSkills())
						{
							CButtonSprite skillButton = new CButtonSprite(skill.getName());

							/*
								Posición de botones:
								X = Ancho de pantalla - (Ancho de Botón + Margen)
								Y = Alto de pantalla - (Alto de Botón * (Cantidad total de Skills - Cantidad de skills ya mostradas en botones))
							 */
							skillButton.setXY(CGameConstants.SCREEN_WIDTH - (skillButton.getWidth() * 2.5f),
								CGameConstants.SCREEN_HEIGHT - (skillButton.getHeight() * (this.selectedBattleEntity.getSkills().Count - this.skillButtons.Count)));
                            //skillButton.setSortingLayerName("Game");
                            this.skillButtons.Add(skillButton, skill);
						}
					}
                    //for each entry in playerPartyButtons, if the entity is Dead, its key(button) and Value(unit) are 
                    deletebuttonsOfPartyMembers(entry.Key, entry.Value);
                }

				// Botones de skills
				foreach (var entry in this.skillButtons)
				{
					entry.Key.update();
					entry.Key.setVisible(this.selectedBattleEntity != null);

					// Si se hizo click en el botón de skill y la entidad es diferente a la actual
					if (entry.Key.clicked() && this.selectedSkill != entry.Value)
					{
						this.selectedSkill = entry.Value;
					}
				}

				// Botones de Enemigos
				foreach (var entry in this.enemyPartyButtons)
				{
                    //Si no se ha clickeado un boton de skill anteriormente, el boton de enemigo no es visible.
					if (this.selectedSkill == null)
					{
						entry.Key.setVisible(false);
					}
					else
					{
						entry.Key.setVisible(true);
					}

					entry.Key.update();

					if (entry.Key.clicked())
					{
						this.addAction(this.selectedBattleEntity, this.selectedSkill, entry.Value);

						//Esconder el botón de el jugador que ya escogió accion
						foreach (var playerButton in this.playerPartyButtons)
						{
							if(playerButton.Value == this.selectedBattleEntity)
							{
								playerButton.Key.setVisible(false);
							}	
						}

						this.selectedSkill = null;
						this.selectedBattleEntity = null;
					}
				}
            break;

            case BattleState.PERFORMING_ACTIONS:
				if (this.selectedActions.Count <= 0)
				{
					this.setState(BattleState.SELECTING_ACTIONS);
				}
				else
				{

					this.selectedActions[0].update();

					if(this.selectedActions[0].getState() == Action.FINISHED)
					{
						Debug.Log("Accion: Terminada");
						this.selectedActions[0].destroy();
						this.selectedActions.RemoveAt(0);
					}
				}
            break;
        }

		foreach (var player in this.playerParty)
		{
			player.update();
		}

		foreach (var enemy in this.enemyParty)
		{
			enemy.update();
		}

        //se encarga de que cuando una abilidad que tiene efecto tardio y se esta performeando, se cargue la animacion
        //mTurnActionManager.update();
    }

    override public void render()
    {
        // Botones de player party
		foreach (var entry in this.playerPartyButtons)
		{
			entry.Key.render();
		}

		// Botones de skills
		foreach (var entry in this.skillButtons)
		{
			entry.Key.render();
		}

		// Botones de enemy party
		foreach (var entry in this.enemyPartyButtons)
		{
			entry.Key.render();
		}

		foreach (var player in this.playerParty)
		{
			player.render();
		}

		foreach (var enemy in this.enemyParty)
		{
			enemy.render();
		}

        //mTurnActionManager.render();
    }

    override public void destroy()
    {
        //mTurnActionManager.destroy();
        //mTurnActionManager = null;
		
		mBackground.destroy();

		foreach (var enemy in this.enemyParty)
		{
			enemy.destroy();
		}

		foreach (var player in this.playerParty)
		{
			player.destroy();
		}

		foreach(var action in this.selectedActions)
		{
			action.destroy();
		}

		this.enemySelectedActions = null;

		foreach(var entry in this.skillButtons)
		{
			entry.Key.destroy();
		}

		foreach(var entry in this.enemyPartyButtons)
		{
			entry.Key.destroy();
		}
		
		foreach(var entry in this.playerPartyButtons)
		{
			entry.Key.destroy();
		}
    }

    protected void addAction(BattleEntity caster, Skill skill, BattleEntity target)
    {
        Action action = new Action(caster, skill, target);
        this.selectedActions.Add(action);

		// Si la cantidad de acciones es igual a la cantidad de la player party entonces el jugador ya terminó de elegir acciones
		// Ahora hay que hacer que los enemigos elijan las suyas
        if(this.selectedActions.Count == this.playerParty.Count)
        {
			foreach(var enemy in this.enemyParty)
			{
				this.selectedActions.Add(enemy.decideAction(this.playerParty, this.enemyParty));
			}
            this.setState(BattleState.PERFORMING_ACTIONS);
        }

		this.hideEnemyButtons();
		this.hideSkillButtons();
    }

	protected void clearSkillButtons()
	{
		foreach (var skillButton in this.skillButtons)
		{
			skillButton.Key.destroy();
		}
		this.skillButtons.Clear();
	}

	protected void hideSkillButtons()
	{
		foreach (var skillButton in this.skillButtons)
		{
			skillButton.Key.setVisible(false);
		}
	}
    protected void deletebuttonsOfPartyMembers(CButtonSprite aKey, BattleEntity aValue)
    {
        if (aKey.isDead())
        {
            playerPartyButtons.Remove(aKey);
        }
    }

	protected void hideEnemyButtons()
	{
		foreach (var enemyButton in this.enemyPartyButtons)
		{
			enemyButton.Key.setVisible(false);
		}
	}

    override public void setState(int aState)
    {
        base.setState(aState);
        // Si se pasa a selecting actions utilizando setState, suceden las siguientes acciones antes de comenzar el turno.
        if (this.getState() == BattleState.SELECTING_ACTIONS)
        {
			//Volver a mostrar botones escondidos
			foreach (var entry in this.playerPartyButtons)
			{
				entry.Key.setVisible(true);
			}

            //win condition
            this.deadEnemiesCount = 0;
            foreach (var entity in enemyParty)
            {
                if (entity.getHealth() == 0)
                {
                    this.deadEnemiesCount += 1;
                }
            }

			//TODO: Realmente hacer algo en la win y lose condition en vez de setear un estado y nada más.

			// Si toda la party del jugador está muerta, entonces perdió.
			if(this.playerParty.Where( x => x.getHealth() == 0).ToList().Count == this.playerParty.Count)
			{
				this.setState(BattleState.PLAYER_LOST);
				return;
			};

			//Si todos los enemigos están muertos entonces pasamos al estado PLAYER_WON y paramos la funcion
			if(this.deadEnemiesCount == this.enemyParty.Count)
			{
				this.setState(BattleState.PLAYER_WON);
				return;
			}
			else if(this.deadEnemiesCount > 0)
			{
				//Si hay algún enemigo muerto entonces lo revivimos a full vida porque estamos re tryhard
				foreach (var entity in enemyParty)
				{
					if (entity.getHealth() == 0)
					{
						entity.setHealth(entity.getMaxHealth());
					}
				}	
			}
			//TODO logica del manager de efectos por turno (que hagan su efecto una vez por turno, que se bajen su variable turnos y si eso es igual a 0 eliminarlo del manager.)
        }
    }
}
