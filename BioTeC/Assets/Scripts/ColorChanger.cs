using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    public Color ReadColor(string color)
    {
        switch(color)
        {
            case "blue":
                return new Color(
                    r: Random.Range(0f, 0f),
                    g: Random.Range(0f, 0f),
                    b: Random.Range(1f, 1f));

            case "black":
                return new Color(
                    r: Random.Range(0f, 0f),
                    g: Random.Range(0f, 0f),
                    b: Random.Range(0f, 0f));
        }
        return new Color(
            r: Random.Range(0f, 1f),
            g: Random.Range(0f, 1f),
            b: Random.Range(0f, 1f));       
    }

    public void ChangeColor(string color)
    {
        _renderer.GetPropertyBlock(_propBlock);

        _propBlock.SetColor(name: "Color", value: ReadColor(color));

        _renderer.SetPropertyBlock(_propBlock);
    }
}
