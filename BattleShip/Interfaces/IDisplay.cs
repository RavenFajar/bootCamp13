public interface IDisplay
{
    void ShowMenu();
    void ShowMessage(string message);
    void ShowBoard(IBoard board);
    void ShowAttackBoard(IBoard board);
    void ShowFleet(List<IShip> ships);
    public void ShipPlacementPhase(IPlayer player);
    void ShowShipPlacementInfo(Ship ship);
    public void FinalShipPlacement(IPlayer player, IBoard board);
    public void InvalidShipPlacement();
    public void InvalidFormat();
    public void PlacementSuccess(IShip ship);
    public void AttackPhase(IPlayer player, IPlayer targetPlayer, Dictionary<IPlayer, List<IShip>> _playerShips, IBoard board);
    public void OutOfBounds();
    public void InvalidAttack();
    public void AlreadyAttack();
    public void NextPhase();
    public void ShipSunk(IShip ship);

}