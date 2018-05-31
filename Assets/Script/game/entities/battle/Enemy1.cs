public class Enemy1 : BattleEntity
{
    public  Enemy1()
    {
        this.setName("prototype_Boss");

        this.setMaxHealth(500);
        this.setAttackDamage(35);


        this.skills.Add(new Atacar());
        this.skills.Add(new Curar());
        this.skills.Add(new Corromper());
    }
}