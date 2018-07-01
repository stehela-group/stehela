using UnityEngine;
using UnityEditor;

public class Envenenar : Skill
{

    public Envenenar()
    {
        // Poison the enemy for two turns, deal 100% attackDamage plus 10% attackDamage the following two turns.
        // CD = 3 turns
        this.setSkillName("Envenenar");
        this.setDamage(100);
        this.setPoison(true);

        this.setAffectingTurns(2);
        this.setPoisonPercentage(10);

        this.setCooldown(3);
    }
}
