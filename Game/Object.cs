using Game.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Object
    {
        public Vector3 Position;

        public VAO Vao { get; private set; }
        public IBO Ibo { get; private set; }
        public ShaderProgram ShaderProgram { get; private set; }
        public Texture Texture { get; private set; }

        private List<Vector3> vertices;
        private List<Vector2> texCoords;
        private List<uint> indices;

        private Matrix4 modelMatrix;

        public Object(List<Vector3> vertices, List<Vector2> texCoords, List<uint> indices, string texturePath, string vertexShaderPath, string fragmentShaderPath)
        {
            this.vertices = vertices;
            this.texCoords = texCoords;
            this.indices = indices;

            Vao = new VAO();
            VBO vertexVbo = new VBO(vertices);
            Vao.LinkToVao(0, 3, vertexVbo);
            VBO uvVbo = new VBO(texCoords);
            Vao.LinkToVao(1, 2, uvVbo);

            Ibo = new IBO(indices);

            ShaderProgram = new ShaderProgram(vertexShaderPath, fragmentShaderPath);

            Texture = new Texture(texturePath);

            modelMatrix = Matrix4.Identity;
            Position = modelMatrix.ExtractTranslation();
        }

        public void Bind()
        {
            Vao.Bind();
            Ibo.Bind();
            Texture.Bind();
        }

        public void SetModelMatrix(Matrix4 matrix)
        {
            modelMatrix = matrix;
            Position = modelMatrix.ExtractTranslation();
        }

        public Matrix4 GetModelMatrix()
        {
            return modelMatrix;
        }

        public void Delete()
        {
            Vao.Delete();
            Ibo.Delete();
            Texture.Delete();
            ShaderProgram.Delete();
        }
    }
}
