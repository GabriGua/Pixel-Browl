using UnityEngine;
using UnityEngine.Tilemaps;

public class StormTileMap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase stormTileBase;
    [SerializeField] private Transform safezone;

   void Update()
    {
        Vector3 center = safezone.position;
        Vector3 size = safezone.localScale;


        Vector3 minWorld = (center - size) / 2;
        Vector3 maxWorld = (center + size);

        Vector3Int minCell = tilemap.WorldToCell(minWorld);
        Vector3Int maxCell = tilemap.WorldToCell(maxWorld);


        RectInt safeZoneRect = new RectInt(
            minCell.x,
            minCell.y - 3,
            maxCell.x + 1,
            maxCell.y + 9);


        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector2Int cellpos = new Vector2Int(pos.x, pos.y);


            if (!safeZoneRect.Contains(cellpos))
            {
                tilemap.SetTile(pos, stormTileBase);
            }
            else
            {

                tilemap.SetTile(pos, null);
            }

        }
    }
    
}
