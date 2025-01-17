using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    public bool isBuildingMode;
    public BuildableItem activeBuildable;
    public ConstructionLayer constructionLayer;
    [SerializeField] private PreviewLayer previewLayer;
    [SerializeField] private Tilemap Ground;
    [HideInInspector] public int direction = 0;
    public Vector3 offset;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        if (constructionLayer == null)
            return;

        if (isBuildingMode)
        {
            if (Input.GetKeyDown(KeyCode.R))
                direction = direction < 3 ? direction + 1 : 0;
            
            BuildingLogic(mousePosition);
        }
        else
            previewLayer.ClearPreview();
    }

    private void BuildingLogic(Vector3 _mousePosition)
    {
        if (activeBuildable == null)
        {
            previewLayer.ClearPreview();
            return;
        }

        Vector3Int currentCoordinate = constructionLayer.tilemap.WorldToCell(_mousePosition);

        previewLayer.ShowPreview(activeBuildable, _mousePosition, constructionLayer.IsEmpty(_mousePosition, activeBuildable), direction);

        if (Input.GetMouseButtonDown(0))
            constructionLayer.Build(_mousePosition, activeBuildable, direction);
    }

}
