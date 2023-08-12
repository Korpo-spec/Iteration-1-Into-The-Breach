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
        Propagate(root, maximizingFaction);
    }

    private void Expand(MinMaxNode node, Faction maximizingFaction, int maxLevel)
    {
        if (node.level < maxLevel && !node.gameState.gameOver)
        {
            foreach (var move in node.gameState.GetMoves())
            {
                GameState childState = node.gameState.GetChildGameState(move);
                MinMaxNode childNode = new MinMaxNode(childState, node.level + 1, move);
                Expand(childNode, maximizingFaction, maxLevel);
                node.children.Add(childNode);
            }
        }
        else
        {
            node.IsLeaf = true;
        }
    }

    private void Propagate(MinMaxNode node, Faction maximizingFaction)
    {
        if (node.IsLeaf)
        {
            node.value = node.gameState.Evaluate(maximizingFaction, node.gameState.GetNextFactionIgnoreEnergy(maximizingFaction));
        }
        else
        {
            foreach (MinMaxNode child in node.children)
            {
                Propagate(child, maximizingFaction);
            }

            if (node.gameState._stateFaction == maximizingFaction)
            {
                node.value = node.GetMinValue();
                
                
            }
            else
            {
                node.value = node.GetMaxValue();
            }
        }
    }
    
    
}
