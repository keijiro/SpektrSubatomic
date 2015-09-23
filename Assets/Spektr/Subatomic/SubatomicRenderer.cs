//
// Subatomic - geometric mirroring effect
//
using UnityEngine;
using UnityEngine.Rendering;

namespace Spektr
{
    [ExecuteInEditMode]
    [AddComponentMenu("Spektr/Subatomic")]
    public class SubatomicRenderer : MonoBehaviour
    {
        #region Editable Properties

        [SerializeField]
        Mesh _mesh;

        [SerializeField]
        Material[] _materials = new Material[1];

        #endregion

        #region Public Properties

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

        #endregion

        #region Private Properties and Functions

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

        #endregion

        #region MonoBehaviour Functions

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

        #endregion
    }
}
