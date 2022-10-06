using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField]                  private Transform       trackPart_start;
    [SerializeField]                  private Transform       trackPart_regular;
    [SerializeField]                  private Transform       trackPart_finish;
    [SerializeField]                  private TrackPoint      trackPoint_prefab;
    [SerializeField]                  private float           offset;
    [SerializeField]                  private int             length;
    [SerializeField, HideInInspector] private List<Transform> segments;

    public int Size => length;

    public Vector3 GetFirst(float height)
    {
        var point = segments.First().position;
        point.y = height;
        return point;
    }


    public Vector3 GetLast(float height)
    {
        var point = segments.Last().position;
        point.y = height;
        return point;
    }


    [Button("REGENERATE", DirtyOnClick = true)]
    private void Generate()
    {
        segments ??= new List<Transform>();

        var count = segments.Count;

        for (var i = 0; i < count; i++)
        {
            if (!segments[i]) continue;
            DestroyImmediate(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(Spawn(trackPart_start, 0));

        for (var i = 0; i < length; i++)
        {
            segments.Add(Spawn(trackPart_regular, offset + (offset * i)));
        }

        segments.Add(Spawn(trackPart_finish, offset * segments.Count));
    }


    public void GeneratePoints(ColorSettings colorSettings, int minStack,   int maxStack,     float selfSpacing,
        int                                  minSpacing,    int maxSpacing, int  variantSeed)
    {
        var beginFrom   = segments[3].position;
        var finishAt    = segments[^3].position;
        var trackLength = (finishAt - beginFrom).magnitude;
        var count       = trackLength / selfSpacing;

        var currentStack   = Random.Range(minStack, maxStack);
        var currentSpacing = Random.Range(minSpacing, maxSpacing);
        var currentVariant = variantSeed;
        var nextPosition   = beginFrom;

        for (var i = 0; i < count; i++)
        {
            nextPosition = beginFrom + Vector3.forward * i * selfSpacing;
            
            if (currentStack >= 0)
            {
                var newPoint = Instantiate(trackPoint_prefab, nextPosition, Quaternion.identity);
                newPoint.Generate(colorSettings, currentVariant);

                currentStack--;
            }
            else
            {
                currentSpacing--;

                if (currentSpacing < 0)
                {
                    currentStack   = Random.Range(minStack, maxStack);
                    currentSpacing = Random.Range(minSpacing, maxSpacing);
                    currentVariant++;
                }
            }
            
            
        }
    }


    private Transform Spawn(Transform original, float step)
    {
        var obj = Instantiate(original, transform);
        obj.localPosition = new Vector3(0, 0, -step);
        return obj;
    }
}