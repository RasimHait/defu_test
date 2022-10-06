using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPoint : MonoBehaviour
{
    [SerializeField] private TrackPointElement                    unitPrefab;
    [SerializeField] private float                                width;
    private                  Dictionary<TrackPointElement, Color> elements = new Dictionary<TrackPointElement, Color>();
    private static readonly  int                                  color    = Shader.PropertyToID("_Color");
    public                   bool                                 isActive = true;


    public void Generate(ColorSettings colorSettings, int seed)
    {
        var colors       = colorSettings.unitColors.Randomize(seed);
        var positionFrom = -width / 2;
        var step         = width / colors.Count;
        
        for (var i = 0; i < colors.Count; i++)
        {
            var newElement = Instantiate(unitPrefab, transform);
            newElement.Initialize(this, out var elementRenderer);
            
            var block = new MaterialPropertyBlock();
            elementRenderer.GetPropertyBlock(block);
            block.SetColor(color, colors[i]);
            elementRenderer.SetPropertyBlock(block);

            var targetPosition = Vector3.zero;
            targetPosition.x = step / 2 + positionFrom + step * i;

            newElement.transform.localPosition = targetPosition;
            
            elements.Add(newElement, colors[i]);
        }
    }


    public Color GetColor(TrackPointElement element) => elements[element];
}