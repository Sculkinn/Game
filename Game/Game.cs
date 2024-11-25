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

        public List<Object> objects = new List<Object>();

        Camera camera;

        Object cube1;
        Object cube2;
        Object cube3;

        public Object floor; //needs to be used by physics

        int width, height;

        public bool GlobalGravity = true;

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

        protected override async void OnLoad()
        {
            base.OnLoad();

            Physics physics = new(this);

            Matrix4 Translation1 = Matrix4.CreateTranslation(0f, 0f, -3f);
            Matrix4 Translation2 = Matrix4.CreateTranslation(0f, 0f, -6f);
            Matrix4 Translation3 = Matrix4.CreateTranslation(0f, 0f, 0f);
            Matrix4 Translation4 = Matrix4.CreateTranslation(0f, -10f, -6f); //y=-0.6f

            cube1 = new Object(vertices, texCoords, indices, "Green.png", "Default.vert", "Default.frag", size, Translation1, 3);
            cube2 = new Object(vertices, texCoords, indices, "Blue.png", "Default.vert", "Default.frag", size, Translation2, 5);
            cube3 = new Object(vertices, texCoords, indices, "Red.png", "Default.vert", "Default.frag", size, Translation3, 1);
            floor = new Object(Fvertices, texCoords, indices, "Black.png", "Default.vert", "Default.frag", fsize, Translation4, 1);
            
            objects.Add(cube3);
            objects.Add(cube2);
            objects.Add(cube1);
            objects.Add(floor);

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;

            for (int i = 0; i < objects.Count; i++)
            {
                Object obj = objects[i];
                obj.SetModelMatrix(obj.Translation);
            }

            while(GlobalGravity)
            {
                await Task.Delay(15);
                physics.ApplyGravity(objects);
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

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(1f, 1f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

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

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            
            if(!cube1.GravityAffected)
            {
                if(cube1.CanMove)
                {
                    if (input.IsKeyDown(Keys.Left))
                    {
                        cube1.Move(0f, 0f, 0.002f);
                    }
                    if (input.IsKeyDown(Keys.Right))
                    {
                        cube1.Move(0f, 0f, -0.002f);
                    }
                }
            }

            if(!cube2.GravityAffected)
            {
                if(cube2.CanMove)
                {
                    if (input.IsKeyDown(Keys.Up))
                    {
                        cube2.Move(0f, 0f, 0.002f);
                    }
                    if (input.IsKeyDown(Keys.Down))
                    {
                        cube2.Move(0f, 0f, -0.002f);
                    }
                }
            }
        }
    }
}