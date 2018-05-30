public class Enemy2 : BattleEntity
{
    public  Enemy2()
    {
        this.setName("Enemy 1");
        this.skills.Add(new Atacar());
    }
}