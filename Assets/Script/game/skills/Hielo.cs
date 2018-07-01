using UnityEngine;
using UnityEditor;

public class Hielo : Skill
{

    public Hielo()
    {
        // Stun the enemy for a turn, deal 70% attackDamage
        // CD = 2 turns
        this.setSkillName("Hielo");
        this.setDamage(70);
        this.setStun(true);

        this.setAffectingTurns(1);

        this.setCooldown(2);
    }
}
