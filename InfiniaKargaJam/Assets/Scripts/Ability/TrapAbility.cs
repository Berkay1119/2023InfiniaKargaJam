
public class TrapAbility:Ability
{
    public override void Use(Player player)
    {
        SpawnableManager.Instance.Spawn(player.GetTrap(),player.GetCurrentTile());
        Destroy(gameObject);
    }
}
