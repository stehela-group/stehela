using UnityEngine;
using System.Collections.Generic;
class BattleState : CGameState
{
	const int SELECTING_ACTIONS = 0;
	const int PERFORMING_ACTIONS = 1;

    // Equipo del jugador
	protected List<BattleEntity> playerParty = new List<BattleEntity>();

	// Equipo Enemigo
	protected List<BattleEntity> enemyParty = new List<BattleEntity>();

	// Acciones a realizar este turno
	protected List<Action> selectedActions = new List<Action>();

	// Entidad del jugador seleccionada en este momento
	protected BattleEntity selectedBattleEntity = null;

    // Entidad de skill seleccionada en este momento
    protected Skill selectedSkill = null; 

    protected Dictionary<CButtonSprite, Skill> skillButtons = new Dictionary<CButtonSprite, Skill>();

    protected Dictionary<CButtonSprite, BattleEntity> playerPartyButtons = new Dictionary<CButtonSprite, BattleEntity>();

    protected Dictionary<CButtonSprite, BattleEntity> enemyPartyButtons = new Dictionary<CButtonSprite, BattleEntity>();

    public BattleState()
    {
		
        this.playerParty.Add(new Player());
		this.playerParty.Add(new Companion1());

		
        this.enemyParty.Add(new Enemy1());
		this.enemyParty.Add(new Enemy2());
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
							skillButton.setXY(CGameConstants.SCREEN_WIDTH - (skillButton.getWidth() + 50),
								CGameConstants.SCREEN_HEIGHT - (skillButton.getHeight() * (this.selectedBattleEntity.getSkills().Count - this.skillButtons.Count)));
							this.skillButtons.Add(skillButton, skill);
						}
					}
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
            break;
        }
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
    }

    override public void destroy()
    {
    }

    protected void addAction(BattleEntity caster, Skill skill, BattleEntity target)
    {
        Action action = new Action(caster, skill, target);
        this.selectedActions.Add(action);

        if(this.selectedActions.Count == this.playerParty.Count)
        {
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

	protected void hideEnemyButtons()
	{
		foreach (var enemyButton in this.enemyPartyButtons)
		{
			enemyButton.Key.setVisible(false);
		}
	}
}
