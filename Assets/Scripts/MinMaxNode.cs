using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxNode
{
    public int level;
    public UnitMove move;
    public GameState gameState;
    public List<MinMaxNode> children;
    public int value;

    public MinMaxNode(GameState gameState, int level)
    {
        this.gameState = gameState;
        this.level = level;
    }
    
    
}
