﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UniSharper.Rendering.DataParsers;
using UnityEngine;

namespace UniSharper.Rendering
{
    /// <summary>
    /// Defines the data format of tiling sheet.
    /// </summary>
    public enum TilingSheetDataFormat
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// The JSON format for Unity engine.
        /// </summary>
        UnityJson
    }

    /// <summary>
    /// The class <see cref="TextureTilingRenderer"/> provides rendering method for texture tiling.
    /// </summary>
    /// <seealso cref="MonoBehaviour"/>
    public class TextureTilingRenderer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private bool AutoUpdateMeshUV = false;

        [SerializeField]
        private TextAsset dataFileAsset = null;

        [SerializeField]
        private TilingSheetDataFormat dataFormat = TilingSheetDataFormat.None;

        private Mesh mesh;

        private Vector2[] meshOriginalUV;

        [SerializeField]
        private string textureTilingName;

        private Dictionary<string, Rect> tilingData;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the name of the texture tiling.
        /// </summary>
        /// <value>The name of the texture tiling.</value>
        public string TextureTilingName
        {
            get
            {
                return textureTilingName;
            }
        }

        private Mesh Mesh
        {
            get
            {
                if (!mesh)
                {
                    MeshFilter meshFilter = GetComponent<MeshFilter>();

                    if (meshFilter)
                    {
                        mesh = meshFilter.mesh;
                    }
                }

                return mesh;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The data.</param>
        public void LoadData(string name, string data)
        {
            if (dataFormat != TilingSheetDataFormat.None)
            {
                ITilingSheetDataParser parser = TilingSheetDataParser.CreateParser(dataFormat);
                tilingData = parser.ParseData(name, data);
            }
        }

        /// <summary>
        /// Updates the mesh UV information with specified name of texture tiling.
        /// </summary>
        /// <param name="textureTilingName">Name of texture tiling.</param>
        public void UpdateMeshUV(string textureTilingName)
        {
            this.textureTilingName = textureTilingName;
            UpdateMeshUV();
        }

        /// <summary>
        /// Updates the mesh UV information.
        /// </summary>
        public void UpdateMeshUV()
        {
            if (tilingData == null || !Mesh || Mesh.uv == null)
            {
                return;
            }

            // Copy mesh original UV
            if (meshOriginalUV == null || meshOriginalUV.Length == 0)
            {
                meshOriginalUV = new Vector2[Mesh.uv.Length];
                Array.Copy(Mesh.uv, meshOriginalUV, Mesh.uv.Length);
            }

            // Change UV of mesh
            if (tilingData.ContainsKey(textureTilingName))
            {
                Rect rect = tilingData[textureTilingName];
                Vector2[] uvs = new Vector2[Mesh.uv.Length];

                for (int i = 0, length = uvs.Length; i < length; i++)
                {
                    uvs[i].x = rect.x + meshOriginalUV[i].x * rect.width;
                    uvs[i].y = rect.y + meshOriginalUV[i].y * rect.height;
                }

                Mesh.uv = uvs;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (dataFileAsset)
            {
                LoadData(dataFileAsset.name, dataFileAsset.text);
            }
        }

        /// <summary>
        /// Executes .
        /// </summary>
        [ContextMenu("Execute")]
        private void Execute()
        {
            if (dataFileAsset && !string.IsNullOrEmpty(textureTilingName))
            {
                LoadData(dataFileAsset.name, dataFileAsset.text);
                UpdateMeshUV();
            }
        }

        private void Start()
        {
            if (AutoUpdateMeshUV)
            {
                UpdateMeshUV();
            }
        }

        #endregion Methods
    }
}