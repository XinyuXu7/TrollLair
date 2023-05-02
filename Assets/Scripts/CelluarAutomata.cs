using UnityEngine;
using System.Collections;
namespace CS.MapGeneration
{
    public class CelluarAutomata
    {
        private static int[,] offset = {
            { -1, -1 }, { -1, 0 }, { -1, 1 },
            {0,-1 },    {0,1 },
            {1,-1 }, {1,0 }, {1,1 }
        };
        private static int GetNeighbourCount(bool[,] map, int x, int y)
        {
            int res = 0;
            for (int i = 0; i < offset.GetLength(0); i++)
            {
                int nx = x + offset[i, 0];
                int ny = y + offset[i, 1];
                if (nx <= 0 || ny <= 0 || nx >= map.GetLength(0) || ny >= map.GetLength(1) || map[nx, ny])
                    res++;
            }
            return res;
        }

        public void Iterate(bool[,] map)
        {
            bool[,] copy = map.Clone() as bool[,];
            for (int i = 0; i < copy.GetLength(0); i++)
            {
                for (int j = 0; j < copy.GetLength(1); j++)
                {
                    map[i, j] = Rule(copy, i, j);
                }
            }
        }

        protected static bool Rule(bool[,] map, int x, int y)
        {
            if (!map[x, y] && GetNeighbourCount(map, x, y) > 4)
                return true;
            else if (map[x, y] && GetNeighbourCount(map, x, y) < 4)
                return false;
            return map[x, y];
        }
    }
}
