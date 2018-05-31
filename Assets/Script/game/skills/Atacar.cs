using UnityEngine;
using UnityEditor;

public class Atacar : Skill
{

    public Atacar()

        //Attacks the target enemy with a strike, dealing caster's basic attackDamage
    {
        this.setSkillName("Atacar");
        this.setDamage(100);
    }
}
