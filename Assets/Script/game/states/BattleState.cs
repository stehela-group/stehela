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
	protected BattleEntity selectedBattleEntity;

    protected Dictionary<CButtonSprite, BattleEntity> battleEntitySelector = new Dictionary<CButtonSprite, BattleEntity>();


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
            selectEntity.setXY( 200, (selectEntity.getHeight() * this.battleEntitySelector.Count) + 50);
            this.battleEntitySelector.Add(selectEntity, entity);
        }
    }

    override public void update()
    {
        foreach (var item in battleEntitySelector)
        {
            item.Key.update();
        }
    }

    override public void render()
    {
		foreach (var item in battleEntitySelector)
		{
			item.Key.render();
		}
    }

    override public void destroy()
    {
    }
}
