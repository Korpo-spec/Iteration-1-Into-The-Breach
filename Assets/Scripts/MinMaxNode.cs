using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MinMaxNode
{
    public bool IsLeaf;
    public int level;
    public UnitMoveData move;
    public GameState gameState;
    public List<MinMaxNode> children;
    public int value;

    public MinMaxNode(GameState gameState, int level)
    {
        this.gameState = gameState;
        this.level = level;
        children = new List<MinMaxNode>();
    }
    public MinMaxNode(GameState gameState, int level, UnitMoveData move)
    {
        this.gameState = gameState;
        this.level = level;

        this.move = move;
        children = new List<MinMaxNode>();
    }

    public int GetMaxValue()
    {
        MinMaxNode maxNode = children[0];
        foreach (var child in children)
        {
            if (child.value > maxNode.value)
            {
                maxNode = child;
            }
        }

        return maxNode.value;
    }
    
    public MinMaxNode GetMaxNode()
    {
        MinMaxNode maxNode = children[0];
        foreach (var child in children)
        {
            if (child.value > maxNode.value)
            {
                maxNode = child;
            }
        }

        return maxNode;
    }
    
    public int GetMinValue()
    {
        MinMaxNode minNode = children[0];
        foreach (var child in children)
        {
            if (child.value < minNode.value)
            {
                minNode = child;
            }
        }

        return minNode.value;
    }
    
}
