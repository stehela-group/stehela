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
    protected BattleEntity selectedSkill = null; 

    protected Dictionary<CButtonSprite, Skill> battleEntitySkillsButtons = new Dictionary<CButtonSprite, Skill>();

    protected Dictionary<CButtonSprite, BattleEntity> battleEntityButtons = new Dictionary<CButtonSprite, BattleEntity>();

    protected Dictionary<CButtonSprite, BattleEntity> battleEntityEnemyButtons = new Dictionary<CButtonSprite, BattleEntity>();

    public BattleState()
    {
		
        this.playerParty.Add(new Player());
		this.playerParty.Add(new Companion1());

		
        this.enemyParty.Add(new Enemy1());
		this.enemyParty.Add(new Enemy2());
    }

    override public void init()
    {
        foreach (var entity in playerParty)
        {
            CButtonSprite selectEntity = new CButtonSprite(entity.getName());
            selectEntity.setXY( 200, (selectEntity.getHeight() * this.battleEntityButtons.Count) + 50);
            this.battleEntityButtons.Add(selectEntity, entity);
        }
    }

    override public void update()
    {
        // Botones de seleccion de personaje
        foreach (var entry in this.battleEntityButtons)
        {
            // entry.Key = CButtonSprite
            // entry.Value = BattleEntity
            entry.Key.update();

            //Si se hizo click en el botón y la entidad es diferente a la actual
            if (entry.Key.clicked() && this.selectedBattleEntity != entry.Value)
            {
                this.selectedBattleEntity = entry.Value;

                // Destruimos botones antiguos
                foreach (var skillButton in this.battleEntitySkillsButtons)
                {
                    skillButton.Key.destroy();
                }
                this.battleEntitySkillsButtons.Clear();

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
                        CGameConstants.SCREEN_HEIGHT - (skillButton.getHeight() * (this.selectedBattleEntity.getSkills().Count - this.battleEntitySkillsButtons.Count)));
                    this.battleEntitySkillsButtons.Add(skillButton, skill);
                }
            }
            // Si se hizo click en el botón de skill y la entidad es diferente a la actual
            else if (entry.Key.clicked() && this.selectedBattleEntity != entry.Value)
            {
                this.selectedBattleEntity = entry.Value;

                // Agregamos botones con los enemigos de la entidad
                foreach (var entity in playerParty)
                {
                    CButtonSprite selectEntity = new CButtonSprite(entity.getName());

                    /*
                        Posición de botones:
                        X = Ancho de pantalla - (Ancho de Botón + Margen)
                        Y = Alto de pantalla - (Alto de Botón * (Cantidad total de Skills - Cantidad de skills ya mostradas en botones))
                     */
                    selectEntity.setXY(500, (selectEntity.getHeight() * this.battleEntityButtons.Count) + 40);
                    this.battleEntityButtons.Add(selectEntity, entity);
                }
            }
        }

        // Botones de skills
        foreach (var entry in this.battleEntitySkillsButtons)
        {
            entry.Key.update();
        }
        // Botones de Enemigos
        foreach (var item in this.battleEntityButtons)
        {
            item.Key.update();
        }
    }

    override public void render()
    {
        // Botones de seleccion de personaje
		foreach (var item in this.battleEntityButtons)
		{
			item.Key.render();
		}

		// Botones de skills
		foreach (var entry in this.battleEntitySkillsButtons)
		{
			entry.Key.render();
		}
    }

    override public void destroy()
    {
    }
}
