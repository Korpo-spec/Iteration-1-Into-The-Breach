using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxNode
{
    private int level;
    private UnitMove move;
    private List<MinMaxNode> children;
    private int value;
}
