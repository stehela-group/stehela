using UnityEngine;
using UnityEditor;

public class Corromper : Skill
{

    public Corromper()
    {
        // Corrupt the enemy for two turns, deal 100% attackDamage and reduce 10%  of the target's attackDamage the following two turns.
        // CD = 3 turns
        this.setSkillName("Corromper");
        this.setDamage(100);
        this.setAttackDebuff(true);

        this.setAffectingTurns(2);
        this.setAttackDebuffPercentage(10);

        this.setCooldown(3);
    }
}
