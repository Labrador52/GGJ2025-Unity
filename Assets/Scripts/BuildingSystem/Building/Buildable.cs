using UnityEngine;

[SerializeField]
public class Buildable
{
    public BuildableItem buildableType;
    public GameObject gameObject;
    public Vector3Int coordinates;
    public int direction;

    public Buildable(BuildableItem _type, Vector3Int _coordinates, int _direction, GameObject _gameobject = null)
    {
        buildableType = _type;
        coordinates = _coordinates;
        gameObject = _gameobject;
        direction = _direction;
    }
}
