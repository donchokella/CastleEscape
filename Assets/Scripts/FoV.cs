using UnityEngine;

public class FoV : MonoBehaviour
{
    [SerializeField] private Material visionConeMaterial;
    [SerializeField] private float visionRange;
    [SerializeField] private float visionAngle;
    [SerializeField] private LayerMask visionObstructingLayer;
    [SerializeField] private int visionConeResolution = 120;

    private MeshFilter meshFilter;
    private Mesh visionConeMesh;
    private Vector3[] vertices;
    private int[] triangles;
    private float[] sineValues;
    private float[] cosineValues;

    public event System.Action OnLose; // ???

    private void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = visionConeMaterial;
        visionConeMesh = new Mesh();

        vertices = new Vector3[visionConeResolution + 1];
        triangles = new int[(visionConeResolution - 1) * 3];
        sineValues = new float[visionConeResolution];
        cosineValues = new float[visionConeResolution];
        InitializeValues();
    }

    private void Update()
    {
        DrawVisionCone();
    }

    private void InitializeValues()
    {
        // Calculate sine and cosine values for each angle in the vision cone
        float currentAngle = -visionAngle / 2;
        float angleIncrement = visionAngle / (visionConeResolution - 1);

        for (int i = 0; i < visionConeResolution; i++)
        {
            sineValues[i] = Mathf.Sin(currentAngle);
            cosineValues[i] = Mathf.Cos(currentAngle);
            currentAngle += angleIncrement;
        }
    }

    private void DrawVisionCone()
    {
        vertices[0] = Vector3.zero;

        for (int i = 0; i < visionConeResolution; i++)
        {
            // Calculate raycast direction and vertex forward vector based on sine and cosine values
            Vector3 raycastDirection = transform.forward * cosineValues[i] + transform.right * sineValues[i];
            Vector3 vertForward = Vector3.forward * cosineValues[i] + Vector3.right * sineValues[i];

            if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, visionRange, visionObstructingLayer))
            {
                // If an obstacle is hit, set the vertex to the intersection point
                vertices[i + 1] = vertForward * hit.distance;

                if (hit.collider.CompareTag("Player"))
                {
                    int playerLevel = hit.collider.GetComponent<PlayerController>().playerLevel;
                    int enemyLevel = transform.parent.gameObject.GetComponent<EnemyController>().enemyLevel;

                    if (playerLevel <= enemyLevel)
                    {
                        OnLose?.Invoke(); // this is not active now

                        // Observer ???
                        hit.collider.GetComponent<PlayerController>().Die.Play(); 
                        hit.collider.GetComponent<PlayerController>().DieP.Play();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Destroy(hit.collider.gameObject, 2f);
                    }
                }
            }
            else
            {
                // If no obstacle is hit, set the vertex to the maximum vision range
                vertices[i + 1] = vertForward * visionRange;
            }
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            // Define the triangles for the mesh
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        // Update the vision cone mesh
        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices;
        visionConeMesh.triangles = triangles;
        meshFilter.mesh = visionConeMesh;
    }
}
