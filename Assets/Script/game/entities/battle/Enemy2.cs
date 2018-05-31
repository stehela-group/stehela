public class Enemy2 : BattleEntity
{
    public  Enemy2()
    {
        this.setName("Enemy 2");
        this.skills.Add(new Atacar());
    }
}