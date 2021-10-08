namespace SB.Common.Items
{
    public class GameItemFactory
    {
        private readonly GameItemsData _gameItemsData;

        public GameItemFactory(GameItemsData gameItemsData)
        {
            _gameItemsData = gameItemsData;
        }

        public GameItem Create(int locationLevel, float magicFindRate)
        {
            return new GameItem();
        }
    }
}