
public class Coin : Bonus
{
    public override void Add()
    {
        GameManager.instance.AddCoin();
    }
}
