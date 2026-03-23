using System;
using System.Numerics;
using Raylib_cs;
using CubeColor = rubiks_cube_simulator.Color;
using RayColor = Raylib_cs.Color;

namespace rubiks_cube_simulator;

class RaylibGui
{
    private Cube _cube = new Cube(true);

    public Cube cube
    {
        set { _cube = value; }
        get { return _cube; }
    }

    public Cube getCube()
    {
        return cube;
    }

    public void setCube(Cube val)
    {
        cube = val;
    }

    public RaylibGui() { }

    public void createWindow()
    {
        Raylib.InitWindow(1200, 800, "Rubiks Cube");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(144);
    }

    public static RayColor CubeColToRay(CubeColor col)
    {
        switch (col)
        {
            case CubeColor.White:
                return RayColor.White;
            case CubeColor.Yellow:
                return RayColor.Yellow;
            case CubeColor.Green:
                return RayColor.Green;
            case CubeColor.Blue:
                return RayColor.Blue;
            case CubeColor.Orange:
                return RayColor.Orange;
            case CubeColor.Red:
                return RayColor.Red;
        }
        throw new InvalidOperationException("This code path should never be reached.");
    }

    public void mainLoop()
    {
        Vector3[] blockPos =
        {
            // top layer
            new(0f, 1f, 1f),
            new(1f, 1f, 1f),
            new(1f, 0f, 1f),
            new(1f, -1f, 1f),
            new(0f, -1f, 1f),
            new(-1f, -1f, 1f),
            new(-1f, 0f, 1f),
            new(-1f, 1f, 1f),
            // middle layer
            new(1f, 1f, 0f),
            new(1f, -1f, 0f),
            new(-1f, -1f, 0f),
            new(-1f, 1f, 0f),
            // bottom layer
            new(0f, 1f, -1f),
            new(1f, 1f, -1f),
            new(1f, 0f, -1f),
            new(1f, -1f, -1f),
            new(0f, -1f, -1f),
            new(-1f, -1f, -1f),
            new(-1f, 0f, -1f),
            new(-1f, 1f, -1f),
        };
        Vector3[] centerPos =
        {
            new(0f, 0f, 1f),
            new(0f, 0f, -1f),
            new(1f, 0f, 0f),
            new(0f, 1f, 0f),
            new(0f, -1f, 0f),
            new(-1f, 0f, 0f),
        };
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(5f, 5f, 5f);
        camera.Target = new Vector3(0f, 0f, 0f);
        camera.Up = new Vector3(0f, 1f, 0f);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        var hblockSize = new Vector3(0.8f, 0.8f, 0.8f);
        var fblockSize = new Vector3(1f, 1f, 1f);
        float plateMove = 0.2f;

        while (!Raylib.WindowShouldClose())
        {
            Block[] blocks = cube.blocks;
            int blocksLen = blocks.Length;

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                Raylib.UpdateCamera(ref camera, CameraMode.ThirdPerson);
            }

            Raylib.BeginDrawing();
            {
                Raylib.ClearBackground(RayColor.RayWhite);

                Raylib.BeginMode3D(camera);
                {
                    for (int i = 0; i < blocksLen; ++i)
                    {
                        Block block = cube.blocks[i];
                        var translate = (Vec3)block.primaryVec.Clone();
                        translate.rotateClockwiseX();
                        translate.rotateClockwiseY();
                        var translate2 = (Vec3)block.secondaryVec.Clone();
                        translate2.rotateClockwiseX();
                        translate2.rotateClockwiseY();
                        Vector3 colFacePos = new(
                            translate.x * plateMove + blockPos[i].X,
                            translate.y * plateMove + blockPos[i].Y,
                            translate.z * plateMove + blockPos[i].Z
                        );
                        Vector3 colFacePos2 = new(
                            translate2.x * plateMove + blockPos[i].X,
                            translate2.y * plateMove + blockPos[i].Y,
                            translate2.z * plateMove + blockPos[i].Z
                        );
                        Raylib.DrawCubeV(blockPos[i], fblockSize, RayColor.Black);
                        Raylib.DrawCubeV(
                            colFacePos2,
                            hblockSize,
                            CubeColToRay(block.getFaceColor(block.secondaryVec))
                        );
                        Raylib.DrawCubeV(
                            colFacePos,
                            hblockSize,
                            CubeColToRay(block.getFaceColor(block.primaryVec))
                        );
                        // RayColor color = RayColor.Maroon;
                        // if (i.Z == 0)
                        // {
                        //     color = RayColor.Blue;
                        // }
                        // else if (i.Z == -1)
                        // {
                        //     color = RayColor.Green;
                        // }
                        // Raylib.DrawCube(i, blockSize, blockSize, blockSize, color);
                        // Raylib.DrawCubeWires(i, blockSize, blockSize, blockSize, RayColor.Black);
                    }
                    foreach (var i in centerPos)
                    {
                        Raylib.DrawCubeV(i, fblockSize, RayColor.Black);
                    }
                    // Raylib.DrawCubeV(new Vector3(1.5f, 0, 0), hblockSize, RayColor.Purple);
                    // Raylib.DrawCubeV(new Vector3(0f, 1.5f, 0), hblockSize, RayColor.Yellow);

                    Raylib.DrawGrid(2, 5f);
                }
                Raylib.EndMode3D();
                Raylib.DrawFPS(10, 10);
            }
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}
