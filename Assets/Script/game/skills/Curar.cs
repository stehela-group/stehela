using UnityEngine;
using UnityEditor;

public class Curar : Skill
{

    public Curar()
    {
        //Heal an ally for 200% of the caster's attackDamage
        //CD = 1 turn

        this.setSkillName("Curar");
        this.setHeal(200);

        this.setCooldown(1);
    }

    override public Target getPossibleTargets()
    {
        return Target.ALLIES;
    }
}
