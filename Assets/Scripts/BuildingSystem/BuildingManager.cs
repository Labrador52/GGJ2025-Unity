using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    public int maxWidth;
    public int maxLength;
    public static BuildingManager instance;
    public bool isBuildingMode;
    public BuildableItem activeBuildable;
    public ConstructionLayer constructionLayer;
    [SerializeField] private PreviewLayer previewLayer;
    [HideInInspector] public int direction = 0;
    public Vector3 offset;
    [Space]
    public Tilemap ground;
    public Tilemap mountain;
    public Tilemap water;
    public Tilemap contaminate;

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

        //Debug.Log(constructionLayer.tilemap.WorldToCell(mousePosition));

        if (constructionLayer == null)
            return;

        if (Mathf.Abs(constructionLayer.tilemap.WorldToCell(mousePosition).x) > maxWidth / 2 ||
            Mathf.Abs(constructionLayer.tilemap.WorldToCell(mousePosition).y) > maxLength / 2)
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

        bool isValid = true;

        Vector3 checkPoint = constructionLayer.tilemap.CellToWorld(constructionLayer.tilemap.WorldToCell(_mousePosition));

        Collider2D hit = Physics2D.OverlapPoint(checkPoint);
        if (hit != null && hit.GetComponent<Tilemap>() != null)
        {
            if (hit.gameObject.tag == "Water" || hit.gameObject.tag == "Mountain")
                isValid = false;
            else
                isValid = constructionLayer.IsEmpty(_mousePosition, activeBuildable);
        }
        else isValid = false;
            
        previewLayer.ShowPreview(activeBuildable, _mousePosition, isValid, direction);

        if (isValid && Input.GetMouseButtonDown(0))
        {
            var currentTile = hit.GetComponent<Tilemap>().GetTile(constructionLayer.tilemap.WorldToCell(_mousePosition));

            constructionLayer.Build(_mousePosition, activeBuildable, direction, currentTile);

            hit.GetComponent<Tilemap>().SetTile(constructionLayer.tilemap.WorldToCell(_mousePosition), null);

        }
    }

}
