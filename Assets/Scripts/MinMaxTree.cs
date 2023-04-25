using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxTree
{
    public MinMaxNode root;
    
    public MinMaxTree(GameState gameState, Faction maximizingFaction, int maxLevel)
    {
        root = new MinMaxNode(new GameState(gameState), 0);
        Expand(root, maximizingFaction, maxLevel);
    }

    private void Expand(MinMaxNode node, Faction maximizingFaction, int maxLevel)
    {
        if (node.level < maxLevel)
        {
            foreach (var move in node.gameState.GetMoves())
            {
                GameState childState = node.gameState.GetChildGameState(move);
                MinMaxNode childNode = new MinMaxNode(childState, node.level + 1, move);
                Expand(childNode, maximizingFaction, maxLevel);
                node.children.Add(childNode);
            }
        }
    }
    
    
}
