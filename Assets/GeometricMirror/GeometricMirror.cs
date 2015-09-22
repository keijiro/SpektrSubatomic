using UnityEngine;

public class GeometricMirror : MonoBehaviour
{
    [SerializeField]
    Mesh _mesh;

    [SerializeField]
    Shader _shader;

    Material[] _materials;

    void DrawMesh(float sx, float sy, float sz, int index)
    {
        var m = transform.localToWorldMatrix;
        m = Matrix4x4.Scale(new Vector3(sx, sy, sz)) * m;
        Graphics.DrawMesh(
            _mesh, m,
            _materials[index], 0
        );
    }

    void Update()
    {
        if (_materials == null)
        {
            _materials = new Material[8];
            for (var i = 0; i < 8; i++)
                _materials[i] = new Material(_shader);

            _materials[1].EnableKeyword("FLIP_X");

            _materials[2].EnableKeyword("FLIP_Y");

            _materials[3].EnableKeyword("FLIP_X");
            _materials[3].EnableKeyword("FLIP_Y");

            _materials[4].EnableKeyword("FLIP_Z");

            _materials[5].EnableKeyword("FLIP_X");
            _materials[5].EnableKeyword("FLIP_Z");

            _materials[6].EnableKeyword("FLIP_Y");
            _materials[6].EnableKeyword("FLIP_Z");

            _materials[7].EnableKeyword("FLIP_X");
            _materials[7].EnableKeyword("FLIP_Y");
            _materials[7].EnableKeyword("FLIP_Z");
        }

        DrawMesh( 1,  1,  1, 0);
        DrawMesh(-1,  1,  1, 1);
        DrawMesh( 1, -1,  1, 2);
        DrawMesh(-1, -1,  1, 3);
        DrawMesh( 1,  1, -1, 4);
        DrawMesh(-1,  1, -1, 5);
        DrawMesh( 1, -1, -1, 6);
        DrawMesh(-1, -1, -1, 7);
    }
}
