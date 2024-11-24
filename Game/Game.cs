using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Game.Graphics;
using System.Reflection;

namespace Game
{
    internal class Game : GameWindow
    {

        List<Vector3> vertices = new List<Vector3>()
        {

            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),

            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
        };

        List<Vector2> texCoords = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };

        List<uint> indices = new List<uint>
        {

            0, 1, 2,

            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20
        };

        Camera camera;

        Object cube1;
        Object cube2;

        Matrix4 trans1;
        Matrix4 trans2;

        int width, height;

        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            cube1 = new Object(vertices, texCoords, indices, "Dirt.png", "Default.vert", "Default.frag");
            cube2 = new Object(vertices, texCoords, indices, "Wood.png", "Default.vert", "Default.frag");

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;

            trans1 = Matrix4.CreateTranslation(0f, 0f, -3f);
            trans2 = Matrix4.CreateTranslation(0f, 0f, -5f);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            cube1.Delete();
            cube2.Delete();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 model1 = cube1.GetModelMatrix();
            Matrix4 model2 = cube2.GetModelMatrix();
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            model1 *= trans1;

            model2 *= trans2;

            cube2.ShaderProgram.Bind();
            cube2.Bind();

            int modelLocation2 = GL.GetUniformLocation(cube2.ShaderProgram.ID, "model");
            int viewLocation2 = GL.GetUniformLocation(cube2.ShaderProgram.ID, "view");
            int projectionLocation2 = GL.GetUniformLocation(cube2.ShaderProgram.ID, "projection");

            GL.UniformMatrix4(modelLocation2, true, ref model2);
            GL.UniformMatrix4(viewLocation2, true, ref view);
            GL.UniformMatrix4(projectionLocation2, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

            cube1.ShaderProgram.Bind();
            cube1.Bind();

            int modelLocation1 = GL.GetUniformLocation(cube1.ShaderProgram.ID, "model");
            int viewLocation1 = GL.GetUniformLocation(cube1.ShaderProgram.ID, "view");
            int projectionLocation1 = GL.GetUniformLocation(cube1.ShaderProgram.ID, "projection");

            GL.UniformMatrix4(modelLocation1, true, ref model1);
            GL.UniformMatrix4(viewLocation1, true, ref view);
            GL.UniformMatrix4(projectionLocation1, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            KeyboardState input = KeyboardState;
            MouseState mouse = MouseState;
            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.Left))
            {
                trans1 = Matrix4.CreateTranslation(trans1.M41, trans1.M42, trans1.M43 + 0.001f);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                trans1 = Matrix4.CreateTranslation(trans1.M41, trans1.M42, trans1.M43 + -0.001f);
            }
        }
    }
}