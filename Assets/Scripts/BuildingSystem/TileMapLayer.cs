using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapLayer : MonoBehaviour
{
    public Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
}