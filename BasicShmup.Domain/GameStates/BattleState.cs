using BasicShmup.Domain.Entities.Ship;

namespace BasicShmup.Domain.GameStates;

public class BattleState(IShipFactory shipFactory) : IBattleState
{
    public void Start()
    {
        _ = shipFactory.Create();
    }
}
