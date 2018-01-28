using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityWeaknesses
{
    All = 0,            // 0
    Sword = 1 << 0,     // 1
    Gun = 1 << 1,       // 2
    Shotgun = 1 << 2,   // 4
    Bombs = 1 << 3,     // 8
    Spear = 1 << 4,     // 16
    Scythe = 1 << 5     // 32
}