public class Enemy1 : BattleEntity
{
    public  Enemy1()
    {
        this.setName("Enemy 1");
        this.skills.Add(new Curar());
        this.skills.Add(new Hielo());
    }
}