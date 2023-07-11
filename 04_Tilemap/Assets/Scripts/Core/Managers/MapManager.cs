using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    Tilemap background;
    Tilemap obstacle;
    GridMap gridMap;

    Spawner[] spawners;

    private void Awake()
    {
        Transform grid = transform.parent;
        Transform tilemap = grid.GetChild(0);
        background = tilemap.GetComponent<Tilemap>();
        tilemap = grid.GetChild(1);
        obstacle = tilemap.GetComponent<Tilemap>();

        gridMap = new GridMap(background, obstacle);

        spawners = GetComponentsInChildren<Spawner>();
    }

    /// <summary>
    /// 스폰 가능한 영역을 미리 찾는데 사용하는 함수
    /// </summary>
    /// <param name="spawner">스폰 가능한 영역을 찾을 스포너</param>
    /// <returns>스포너의 영역 중 스폰 가능한 지역의 리스트</returns>
    public List<Node> CalcSpawnArea(Spawner spawner)
    {
        List<Node> result = new List<Node>();

        Vector2Int min = gridMap.WorldToGrid(spawner.transform.position);
        Vector2Int max = gridMap.WorldToGrid(spawner.transform.position + (Vector3)spawner.size);

        for(int y = min.y; y < max.y;y++)
        {
            for(int x = min.x;x < max.x;x++)
            {
                if(!gridMap.IsWall(x,y))
                {
                    result.Add(gridMap.GetNode(x,y));
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 맵의 그리드 좌표를 월드 좌표로 변경해주는 함수
    /// </summary>
    /// <param name="x">x위치</param>
    /// <param name="y">y위치</param>
    /// <returns>월드 좌표</returns>
    public Vector2 GridToWorld(int x, int y)
    {
        return gridMap.GridToWorld(new Vector2Int(x, y));
    }
}
