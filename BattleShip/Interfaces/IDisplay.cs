public interface IDisplay
{
    void ShowMenu();
    void ShowMessage(string message);
    void ShowBoard(IBoard board);
    void ShowAttackBoard(IBoard board);
    void ShowFleet(List<IShip> ships);
    void ShowShipPlacementInfo(IShip ship);
}