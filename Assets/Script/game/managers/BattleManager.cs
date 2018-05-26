using System.Collections.Generic;

class BattleManager
{
	// Equipo del Jugador
	protected List<BattleEntity> playerParty;

	// Equipo Enemigo
	protected List<BattleEntity> enemyParty;

	// Acciones a realizar este turno
	protected List<Action> selectedActions;

	// Entidad del jugador seleccionada en este momento
	protected BattleEntity selectedBattleEntity;

	const int SELECTING_ACTIONS = 0;
	const int PERFORMING_ACTIONS = 1;

	public BattleManager() 
	{

	}

	public void update()
	{
		
	}

	public void render()
	{

	}

	public void destroy()
	{
        
	}
}