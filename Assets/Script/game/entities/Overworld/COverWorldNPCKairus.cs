using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class COverWorldNPCKairus : CAnimatedSprite
{
    private string portraitAddress;
    private const int STATE_IDLE = 1;

    public COverWorldNPCKairus()
    {
        portraitAddress = "Sprites/NPC/Kairus_Portrait";
        setFrames(Resources.LoadAll<Sprite>("Sprites/KairusOverworld"));
        setName("Overworld NPC Kairus");
        setSortingLayerName("Personajes");
        setScale(6.5f);
        setBounds(0, 0, CGameConstants.SCREEN_WIDTH, CGameConstants.SCREEN_HEIGHT);
        setBoundAction(CGameObject.NONE);
        setState(STATE_IDLE);
    }

    override public void update()
    {
        base.update();
    }

    override public void render()
    {
        base.render();
    }

    override public void destroy()
    {
        base.destroy();
    }

    public void mensaje()
    {
		string[] text = null;
        if(BattleData.lastBattleOutcome == BattleData.BattleOutcome.NO_BATTLE)
        {
            text = new string[] {
                "Te he estado esperando, Yo soy Kairus, el líder de los Tifus y el asesino de la última persona con el concepto de muerte en este mundo, tu padre.",
                "Encontré la fluctuación del espacio que te tenía atrapado hace 13 años y he esperado al dia que salieras de ella.",
                "Lo lamento, aunque mis disculpas no puedan arreglar la atrocidad que he cometido me veo obligado a decirlas de igual manera.",
                "He vivido hasta este momento solo para que tu fueras juez y verdugo de mis acciones.",
                "Si quieres que viva y te ayude en la travesía que estas por recorrer, así será, si por otro lado quieres que page con mi vida, tambien asi sera."

            };
        }
        else if(BattleData.lastBattleOutcome == BattleData.BattleOutcome.WON)
        {
			text = new string[] {
				"¡Los has vencido! Creí que nunca se irían.",
				"*AIU BRBRBRBR* ¿Qué ha sido eso?",
				"Oh no, creo que han venido más."
			};
        }
        else {
			text = new string[] {
				"Te han tirado dentro y quedaste insconsciente...",
                "Hace " + BattleData.battlesLost + " días.",
                "Siguen fuera, no han entrado porque no caben por la puerta."
			};
        }

        DialogManager.startDialog(new Dialog(text, portraitAddress));
    }
    public override void setState(int aState)
    {
        base.setState(aState);

        if (getState() == STATE_IDLE)
        {
            initAnimation(1, 12, 16, true);

        }
    }
}


