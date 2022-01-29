
using System.Linq;
using Board;

namespace Game
{
    public class GameLogic
    {
        private Board.Board _board;
        private ResourceHandler _resourceHandler;
        public const int NukeCost = 5;
        public const int ScoutCost = 3;
        
        public GameLogic(Board.Board board, ResourceHandler resourceHandler)
        {
            _board = board;
            _resourceHandler = resourceHandler;
        }
        

        /// <summary>
        /// Check if node is claimable by player at current state
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsClaimable(Node node)
        {
            if (node.Cost > _resourceHandler.PlayerResources)
            {
                return false;
            }

            if (node.Team == Team.Player)
            {
                return false;
            }

            if (node.Neighbors.Where(neighbor => neighbor != null).Any(neighbor => neighbor.Team == Team.Player && neighbor.Type != NodeType.Trap))
            {
                return true;
            }
            return false;
        }
        
        public bool CanNuke(Node node)
        {
            if (node.Team != Team.Player) return false;
            return NukeCost <= _resourceHandler.PlayerResources;
        }
        
        public bool CanScout(Node node)
        {
            if (node.IsRevealed) return false;
            return ScoutCost <= _resourceHandler.PlayerResources;
        }
    }
}