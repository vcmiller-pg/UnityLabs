using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AreaType
{
    Default, Restaurant, Road
}

public class UnlockArea : MonoBehaviour
{
    public Vector2Int areaSize = Vector2Int.one;
    public bool[] containedSquares = { false };

    public string areaName;
    public AreaType type;
    public bool unlocked;

    public RectInt AreaBounds
    {
        get
        {
            Vector2Int pos = Vector2Int.RoundToInt(new Vector2(transform.position.x, transform.position.z));
            return new RectInt(pos, areaSize);
        }
    }
    
    public RectInt LocalAreaBounds => new RectInt(Vector2Int.zero, areaSize);

    private int ToArrayIndex(Vector2Int pos, Vector2Int size)
    {
        int ax = pos.x;
        int az = pos.y;
        return ax + az * size.x;
    }

#if UNITY_EDITOR
    public void Resize(Vector2Int newSize)
    {
        var oldContents = containedSquares;
        var oldSize = areaSize;
        containedSquares = new bool[newSize.x * newSize.y];
        areaSize = newSize;
        for (int x = 0; x < newSize.x; x++)
        {
            for (int y = 0; y < newSize.y; y++)
            {
                var pos = new Vector2Int(x, y);
                if (pos.x >= oldSize.x || pos.y >= oldSize.y) continue;

                containedSquares[ToArrayIndex(pos, newSize)] = oldContents[ToArrayIndex(pos, oldSize)];
            }
        }
    }

    public void TogglePosition(Vector2Int position)
    {
        ref bool b = ref containedSquares[ToArrayIndex(position, areaSize)];
        b = !b;
    }

    public void SetAllEnabled(bool value)
    {
        for (int i = 0; i < containedSquares.Length; i++)
        {
            containedSquares[i] = value;
        }
    }

    public bool IsPositionEnabled(Vector2Int position)
    {
        return containedSquares[ToArrayIndex(position, areaSize)];
    }
    
    private void OnDrawGizmos()
    {
        // Primary area
        float buffer = 0.05f;
        Gizmos.color = new Color(1, .8f, 1, .5f);
        foreach (var worldPos in AreaBounds.allPositionsWithin)
        {
            var pos = worldPos - AreaBounds.min;
            Vector3 c = new Vector3(worldPos.x, 0, worldPos.y);
            if (IsPositionEnabled(pos))
            {
                Gizmos.DrawCube(c, new Vector3(0.9f, buffer, 0.9f));
            }
        }
    }
#endif
}
