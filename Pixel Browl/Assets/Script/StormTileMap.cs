using UnityEngine;
using UnityEngine.Tilemaps;

public class StormTileMap : MonoBehaviour
{
    
    Tilemap Tilemap;
   [SerializeField] Tile tile;
    
    

    private void Start()
    {
       
        Tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            
            if(collision.tag == "Zone")
            {
                for (int i = 0; i < 42; i++)
                {
                    for(int j = 0; j < 42; j++)
                    {
                        
                    }
                }
            }
        }
    }
}
