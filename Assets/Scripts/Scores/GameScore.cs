using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameScore : IEquatable<GameScore>, IComparable<GameScore> {

    public int score;
    public String name;

    public int CompareTo(GameScore other)
    {
        if (other == null)
            return 0;
        else
            return 1;
    }

    public bool Equals(GameScore other)
    {
        if (other == null)
            return false;
        else
            return (score == other.score && other.name == name);

    }
}
