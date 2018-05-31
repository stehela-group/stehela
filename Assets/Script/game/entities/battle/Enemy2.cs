public class Enemy2 : BattleEntity
{
    public  Enemy2()
    {
        this.setName("prototype_minion");

        this.setMaxHealth(125);
        this.setAttackDamage(10);
        this.skills.Add(new Atacar());
    }
}