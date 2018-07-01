using UnityEngine;
using UnityEditor;

public class Atacar : Skill
{

    //Attacks the target enemy with a strike, dealing caster's basic attackDamage
    
    public Atacar()
    {
        this.setCastTime(1);
        this.setSkillName("Atacar");
        this.setDamage(100);
    }
}
