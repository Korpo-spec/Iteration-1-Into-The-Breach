using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxNode
{
    public int level;
    public UnitMoveData move;
    public GameState gameState;
    public List<MinMaxNode> children;
    public int value;

    public MinMaxNode(GameState gameState, int level)
    {
        this.gameState = gameState;
        this.level = level;
    }
    public MinMaxNode(GameState gameState, int level, UnitMoveData move)
    {
        this.gameState = gameState;
        this.level = level;

        this.move = move;
    }
    
    
}
