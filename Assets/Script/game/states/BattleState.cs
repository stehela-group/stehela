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

    protected Dictionary<CButtonSprite, Skill> battleEntitySkillsButtons = new Dictionary<CButtonSprite, Skill>();

    protected Dictionary<CButtonSprite, BattleEntity> battleEntityButtons = new Dictionary<CButtonSprite, BattleEntity>();


    public BattleState()
    {
		this.playerParty.Add(new BattleEntity("Player", 100));
		this.playerParty.Add(new BattleEntity("Companion 1", 200));
		this.playerParty.Add(new BattleEntity("Companion 2", 50));

		this.enemyParty.Add(new BattleEntity("Enemy 1", 800));
		this.enemyParty.Add(new BattleEntity("Enemy 2", 200));
		this.enemyParty.Add(new BattleEntity("Enemy 3", 25));
    }

    override public void init()
    {
        foreach (var entity in playerParty)
        {
            CButtonSprite selectEntity = new CButtonSprite(entity.getEntityName());
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

            if(entry.Key.clicked())
            {
                this.selectedBattleEntity = entry.Value;
                this.battleEntitySkillsButtons.Clear();
                foreach (var skill in this.selectedBattleEntity.getSkills())
                {
                    CButtonSprite skillButton = new CButtonSprite(skill.getName());
					skillButton.setXY(1600, (skillButton.getHeight() * this.battleEntityButtons.Count) + 50);
                    this.battleEntitySkillsButtons.Add(skillButton, skill);
                }
            }
        }

        // Botones de skills
        foreach (var entry in this.battleEntitySkillsButtons)
        {
            entry.Key.update();
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
			entry.Key.update();
		}
    }

    override public void destroy()
    {
    }
}
