using UnityEngine;

public class UniInput : MonoBehaviour
{
    public Vector2 Axis { get; private set; }

    private                  bool    isDragging;
    private                  Vector2 tempPosition;
    [SerializeField] private float   zeroingSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            tempPosition = Input.mousePosition;
            isDragging   = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Axis       = tempPosition = Vector2.zero;
            isDragging = false;
        }

        if (!isDragging) return;

        var currentPosition = (Vector2)Input.mousePosition;

        Axis = (currentPosition - tempPosition) / 100f;

        tempPosition = Vector2.Lerp(tempPosition, currentPosition, zeroingSpeed * Time.deltaTime);
    }
}