using System;
using Board;

namespace Game
{
    public class ResourceHandler
    {
        public const int MaxResources = 20;
        public int AiResources { get; private set; }
        public int PlayerResources { get; private set; }
        
        public ResourceHandler(int startResources)
        {
            PlayerResources = startResources;
            AiResources = startResources;
        }

        public void AddResources(int amount, Team team)
        {
            switch (team)
            {
                case Team.Player:
                    PlayerResources += amount;
                    if (PlayerResources < 0) PlayerResources = 0;
                    if (PlayerResources > MaxResources) PlayerResources = MaxResources;
                    break;
                case Team.Ai:
                    AiResources += amount;
                    if (AiResources < 0) AiResources = 0;
                    if (AiResources > MaxResources) AiResources = MaxResources;
                    break;
                default:
                    break;
            }
        }

        public void AddResources(Node[] nodes, Team team)
        {
            foreach (var node in nodes)
            {
                if (node.Team == team)
                {
                    AddResources(node.Resoruces, team);
                }
            }
        }
    }
}