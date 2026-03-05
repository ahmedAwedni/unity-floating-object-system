using UnityEngine;

[DisallowMultipleComponent]
public class FloatingObject2D : MonoBehaviour
{
    [Header("Floating Settings")]
    [SerializeField] private float amplitude = 0.5f;  // Height of float
    [SerializeField] private float frequency = 1f;    // Speed of float
    [SerializeField] private Vector2 floatAxis = Vector2.up;

    [Header("Optional Rotation")]
    [SerializeField] private bool enableRotation = false;
    [SerializeField] private float rotationSpeed = 45f;

    private Vector3 startPosition;
    private float randomOffset;

    private void Awake()
    {
        startPosition = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        FloatMotion();
        Rotate();
    }

    private void FloatMotion()
    {
        float offset = Mathf.Sin((Time.time + randomOffset) * frequency) * amplitude;
        Vector3 axis = new Vector3(floatAxis.x, floatAxis.y, 0f).normalized;
        transform.position = startPosition + axis * offset;
    }

    private void Rotate()
    {
        if (!enableRotation) return;

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void ResetStartPosition()
    {
        startPosition = transform.position;
    }

    
    // --- Gizmos Logic ---
    private void OnDrawGizmos()
    {
        // Determine the start point (use current pos in editor if not playing)
        Vector3 origin = Application.isPlaying ? startPosition : transform.position;
        Vector3 direction = new Vector3(floatAxis.x, floatAxis.y, 0f).normalized;

        // Calculate limits
        Vector3 topLimit = origin + direction * amplitude;
        Vector3 bottomLimit = origin - direction * amplitude;

        // Draw the path line
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(topLimit, bottomLimit);

        // Draw small spheres at the endpoints
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(topLimit, 0.05f);
        Gizmos.DrawSphere(bottomLimit, 0.05f);

        // Draw a wire circle to show the 'rest' position
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(origin, 0.1f);
    }

}
