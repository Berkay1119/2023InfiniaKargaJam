
public class TrapAbility:Ability
{
    public override void Use(Player player)
    {
        
        Trap spawnable=(Trap)SpawnableManager.Instance.Spawn(player.GetTrap(),player.GetCurrentTile());
        spawnable.owner = player;
        Destroy(gameObject);
    }
}
