using System;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattleEnemyManager : IBattleManager
    {
        public event EventHandler UnitsPrepared;
        public  IPlayer Player { get; }

        public BattleEnemyManager()
        {
            Player = new GamePlayer(Team.Red);
        }
        
        public void PrepareUnits()
        {
            UnitsPrepared?.Invoke(this, EventArgs.Empty);    
        }
    }
}