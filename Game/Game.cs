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

        List<Vector3> Fvertices = new List<Vector3>()
        {

            new Vector3(-10f, 0.1f, 10f),
            new Vector3(10f, 0.1f, 10f),
            new Vector3(10f, -0.1f, 10f),
            new Vector3(-10f, -0.1f, 10f),

            new Vector3(10f, 0.1f, 10f),
            new Vector3(10f, 0.1f, -10f),
            new Vector3(10f, -0.1f, -10f),
            new Vector3(10f, -0.1f, 10f),

            new Vector3(10f, 0.1f, -10f),
            new Vector3(-10f, 0.1f, -10f),
            new Vector3(-10f, -0.1f, -10f),
            new Vector3(10f, -0.1f, -10f),

            new Vector3(-10f, 0.1f, -10f),
            new Vector3(-10f, 0.1f, 10f),
            new Vector3(-10f, -0.1f, 10f),
            new Vector3(-10f, -0.1f, -10f),

            new Vector3(-10f, 0.1f, -10f),
            new Vector3(10f, 0.1f, -10f),
            new Vector3(10f, 0.1f, 10f),
            new Vector3(-10f, 0.1f, 10f),

            new Vector3(-10f, -0.1f, 10f),
            new Vector3(10f, -0.1f, 10f),
            new Vector3(10f, -0.1f, -10f),
            new Vector3(-10f, -0.1f, -10f),
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

        List<Object> objects = new List<Object>();

        Camera camera;

        Object cube1;
        Object cube2;
        Object cube3;

        Object floor;

        int width, height;

        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;
            this.Title = "Game";

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        private Vector3 fsize = new Vector3(10f, 0.1f, 10f);
        private Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);

        protected override void OnLoad()
        {
            base.OnLoad();

            Matrix4 trans1 = Matrix4.CreateTranslation(0f, 0f, -3f);
            Matrix4 trans2 = Matrix4.CreateTranslation(0f, 0f, -6f);
            Matrix4 trans3 = Matrix4.CreateTranslation(0f, 0f, 0f);
            Matrix4 trans4 = Matrix4.CreateTranslation(0f, -5f, -6f);

            cube1 = new Object(vertices, texCoords, indices, "Green.png", "Default.vert", "Default.frag", size, trans1);
            cube2 = new Object(vertices, texCoords, indices, "Blue.png", "Default.vert", "Default.frag", size, trans2);
            cube3 = new Object(vertices, texCoords, indices, "Red.png", "Default.vert", "Default.frag", size, trans3);
            floor = new Object(Fvertices, texCoords, indices, "Black.png", "Default.vert", "Default.frag", fsize, trans4);

            objects.Add(cube1);
            objects.Add(cube2);
            objects.Add(cube3);
            objects.Add(floor);

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;

            for (int i = 0; i < objects.Count; i++)
            {
                Object obj = objects[i];
                obj.SetModelMatrix(obj.Trans);
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Delete();
            }
        }

        public static bool CheckCollision(Object obj1, Object obj2)
        {
            Vector3 size1 = obj1.Size;
            Vector3 size2 = obj2.Size;

            Vector3 min1 = obj1.Position - size1;
            Vector3 max1 = obj1.Position + size1;

            Vector3 min2 = obj2.Position - size2;
            Vector3 max2 = obj2.Position + size2;

            return (min1.X <= max2.X && max1.X >= min2.X) &&
                   (min1.Y <= max2.Y && max1.Y >= min2.Y) &&
                   (min1.Z <= max2.Z && max1.Z >= min2.Z);
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(1f, 1f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = i + 1; j < objects.Count; j++)
                {
                    if (CheckCollision(objects[i], objects[j]))
                    {
                        objects[i].GravityAffected = false;
                    }
                    else
                    {
                        objects[i].GravityAffected = true;
                    }
                }
            }

            for (int i = 0; i < objects.Count; i++)
            {
                Object obj = objects[i];
                Matrix4 model = obj.GetModelMatrix();

                obj.ShaderProgram.Bind();
                obj.Bind();

                int modelLocation = GL.GetUniformLocation(obj.ShaderProgram.ID, "model");
                int viewLocation = GL.GetUniformLocation(obj.ShaderProgram.ID, "view");
                int projectionLocation = GL.GetUniformLocation(obj.ShaderProgram.ID, "projection");

                GL.UniformMatrix4(modelLocation, true, ref model);
                GL.UniformMatrix4(viewLocation, true, ref view);
                GL.UniformMatrix4(projectionLocation, true, ref projection);

                GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            }

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            KeyboardState input = KeyboardState;
            MouseState mouse = MouseState;
            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);

            foreach (var obj in objects)
            {
                if(obj.GravityAffected)
                {
                    if (obj != floor)
                    {
                        obj.Trans *= Matrix4.CreateTranslation(0f, -0.00075f, 0f);
                        obj.SetModelMatrix(obj.Trans);
                    }
                }
            }

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.Left))
            {
                cube1.Trans *= Matrix4.CreateTranslation(0f, 0f, 0.001f);
                cube1.SetModelMatrix(cube1.Trans);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                cube1.Trans *= Matrix4.CreateTranslation(0f, 0f, -0.001f);
                cube1.SetModelMatrix(cube1.Trans);
            }

            if (input.IsKeyDown(Keys.Up))
            {
                cube2.Trans *= Matrix4.CreateTranslation(0f, 0f, 0.001f);
                cube2.SetModelMatrix(cube2.Trans);
            }
            if (input.IsKeyDown(Keys.Down))
            {
                cube2.Trans *= Matrix4.CreateTranslation(0f, 0f, -0.001f);
                cube2.SetModelMatrix(cube2.Trans);
            }
        }
    }
}