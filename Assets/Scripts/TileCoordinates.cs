using UnityEngine;

[System.Serializable]
public struct TileCoordinates
{
    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public TileCoordinates(int i, int j)
    {
        x = i;
        z = j;
    }

    public static TileCoordinates FromOffset(int x, int z) {
        return new TileCoordinates(x - z / 2, z);
    }

    public static TileCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (Metrics.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (Metrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new TileCoordinates(iX, iZ);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public string ToStringSeparateLines() {
        return $"{X}\n{Y}\n{Z}";
    }
}