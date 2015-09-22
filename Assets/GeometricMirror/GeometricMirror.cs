using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class GeometricMirror : MonoBehaviour
{
    [SerializeField]
    Mesh _mesh;

    [SerializeField]
    Material[] _materials = new Material[1];

    [SerializeField]
    ShadowCastingMode _castShadows;

    public ShadowCastingMode shadowCastingMode {
        get { return _castShadows; }
        set { _castShadows = value; }
    }

    [SerializeField]
    bool _receiveShadows = false;

    public bool receiveShadows {
        get { return _receiveShadows; }
        set { _receiveShadows = value; }
    }

    MaterialPropertyBlock _props;

    void DrawMesh(float sx, float sy, float sz)
    {
        var scale = new Vector3(sx, sy, sz);
        var matrix = Matrix4x4.Scale(scale) * transform.localToWorldMatrix;

        _props.SetVector("_FlipFlags", scale);

        var maxi = Mathf.Min(_mesh.subMeshCount, _materials.Length);
        for (var i = 0; i < maxi; i++)
        {
            Graphics.DrawMesh(
                _mesh, matrix, _materials[i], 0, null, i,
                _props, _castShadows, _receiveShadows);
        }
    }

    void Update()
    {
        if (_mesh == null || _materials == null) return;

        if (_props == null)
            _props = new MaterialPropertyBlock();

        DrawMesh( 1,  1,  1);
        DrawMesh(-1,  1,  1);
        DrawMesh( 1, -1,  1);
        DrawMesh(-1, -1,  1);
        DrawMesh( 1,  1, -1);
        DrawMesh(-1,  1, -1);
        DrawMesh( 1, -1, -1);
        DrawMesh(-1, -1, -1);
    }
}
