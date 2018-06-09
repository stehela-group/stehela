﻿using System;
using UnityEngine;
using System.Collections.Generic;
class BattleState : CGameState
{
	const int SELECTING_ACTIONS = 0;
	const int PERFORMING_ACTIONS = 1;

    private CBackground mBackground;

    private turnActionManager mTurnActionManager;

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

		
        this.enemyParty.Add(new Enemy1());
        this.enemyParty.Add(new Enemy2());

        mTurnActionManager = new turnActionManager();
    }

    override public void init()
    {

        foreach (var entity in this.playerParty)
        {
            CButtonSprite playerButton = new CButtonSprite(entity.getName());
            playerButton.setXY( 200, (playerButton.getHeight() * this.playerPartyButtons.Count) + 50);
            this.playerPartyButtons.Add(playerButton, entity);
        }

		// Agregamos botones con los enemigos de la entidad
		foreach (var entity in this.enemyParty)
		{
			CButtonSprite enemyButton = new CButtonSprite(entity.getName());

			enemyButton.setXY(CGameConstants.SCREEN_WIDTH - (enemyButton.getWidth() + 50),
				(enemyButton.getHeight() * this.enemyPartyButtons.Count) + 40);
			this.enemyPartyButtons.Add(enemyButton, entity);
		}


        mBackground = new CBackground();
        mBackground.setXY(0, 0);
    }

    override public void update()
    {
        switch (this.getState())
        {
            case BattleState.SELECTING_ACTIONS:
                // TODO: add variable to do this only once per turn.
                // cada oponente decide que habilidad utilizar.
                /*
				
				*/
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
							skillButton.setXY(CGameConstants.SCREEN_WIDTH - (skillButton.getWidth() + 50),
								CGameConstants.SCREEN_HEIGHT - (skillButton.getHeight() * (this.selectedBattleEntity.getSkills().Count - this.skillButtons.Count)));
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
						this.selectedSkill = null;
						this.selectedBattleEntity = null;
					}
				}
            break;

            case BattleState.PERFORMING_ACTIONS:
				
				Debug.Log(this.selectedActions.Count);

				if (this.selectedActions.Count <= 0)
				{
					this.setState(BattleState.SELECTING_ACTIONS);
				}
				else
				{

					this.selectedActions[0].update();

					if(this.selectedActions[0].getState() == Action.FINISHED)
					{
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
        mTurnActionManager.update();
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

        mTurnActionManager.render();
    }

    override public void destroy()
    {
        mTurnActionManager.destroy();
        mTurnActionManager = null;
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

        int c = this.getState();
        // Si se pasa a selecting actions utilizando setState, suceden las siguientes acciones antes de comenzar el turno.
        if (c == BattleState.SELECTING_ACTIONS)
        {
            //win condition
            deadEnemiesCount = 0;
            foreach (var entity in enemyParty)
            {
                int h = entity.getHealth();
                if (h == 0)
                {
                    deadEnemiesCount += 1;
                }
            }
            //TODO If deadEnemiesCount == cantidad de enemigos en enemyParty: cortar loop y finalizar la partida.

            //logica de seleccion de habilidad para el oponente.
            foreach (var entity in this.enemyParty)
            {
                entity.clearAvailableSkills();
                entity.checkCooldowns();
                if (!entity.isCasting())
                {
                    entity.setSelectedAction();
                    // si es un heal, el enemigo targetea a un aliado suyo.
                    if (entity.getSelectedAction().getHeal() > 0)
                    {
                        int g;
                        g = CMath.randomIntBetween(1, enemyParty.Count);

                        entity.setTarget(entity.getSelectedAction(), enemyParty[g]);
                    }
                    // de lo contrario targeta al playerParty
                    else
                    {
                        int i;
                        i = CMath.randomIntBetween(1, playerParty.Count);

                        entity.setTarget(entity.getSelectedAction(), playerParty[i]);
                    }
                }
                //agrega al diccionario la entidad y la skill que usará
                enemySelectedActions.Add(entity, entity.getSelectedAction());

                //TODO logica del manager de efectos por turno (que hagan su efecto una vez por turno, que se bajen su variable turnos y si eso es igual a 0 eliminarlo del manager.)

            }
        }
    }
}
