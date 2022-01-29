
using System;
using System.Linq;
using Board;

namespace Game
{
    public class GameLogic
    {
        private Board.Board _board;
        public event Action GameWon;
        public event Action GameLost;
        
        public int NukeCharges { get; private set; }
        public int ScoutCharges { get; private set; }
        public GameLogic(Board.Board board)
        {
            _board = board;
            
            NukeCharges = 1;
            ScoutCharges = 2;
        }
        

        /// <summary>
        /// Check if node is claimable by player at current state
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool CanClaim(Node node)
        {
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
            return NukeCharges > 0;
        }
        
        public bool CanScout(Node node)
        {
            if (node.IsRevealed) return false;
            return ScoutCharges > 0;
        }

        public void ClaimNode(Node node)
        {
            if (!CanClaim(node)) return;
            var type = node.Reveal();
            node.SetOwner(Team.Player);
            if (type == NodeType.Root)
            {
                GameWon.Invoke();
            }
        }
        
        public void NukeNode(Node node)
        {
            if (!CanNuke(node)) return;
            
            foreach (var neighbor in node.Neighbors)
            {
                if(neighbor == null) continue;
                if(neighbor.Type == NodeType.Root) continue;
                neighbor.Nuke();
            }

            NukeCharges--;
            
            _board.EvaluateNodes();
        }

        public void ScoutNode(Node node)
        {
            if (!CanScout(node)) return;

            node.Reveal();
            ScoutCharges--;
        }
    }
}