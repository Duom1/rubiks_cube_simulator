using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using CubeColor = rubiks_cube_simulator.Color;
using RayColor = Raylib_cs.Color;

namespace rubiks_cube_simulator;

class RaylibGui
{
    private Cube _cube = new Cube(true);

    private List<string> _algos = new List<string>();

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

    public void queueAlgo(string algo)
    {
        _algos.Add(algo);
    }

    public void createWindow()
    {
        Raylib.InitWindow(1200, 800, "Rubiks Cube");
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        Raylib.SetTargetFPS(144);
    }

    public static Vector3 translateCubeToRay(Vec3 pos)
    {
        var translate = (Vec3)pos.Clone();
        translate.rotateClockwiseY();
        translate.rotateCounterClockwiseZ();
        translate.rotateCounterClockwiseZ();
        return new(translate.x, translate.y, translate.z);
    }

    public static Vector3 translateBackCubeToRay(Vec3 pos)
    {
        var translate = (Vec3)pos.Clone();
        translate.rotateCounterClockwiseY();
        translate.rotateClockwiseZ();
        translate.rotateClockwiseZ();
        return new(translate.x, translate.y, translate.z);
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
        (Vector3, RayColor)[] centerPos =
        {
            (new(0f, 0f, 1f), RayColor.White),
            (new(0f, 0f, -1f), RayColor.Yellow),
            (new(1f, 0f, 0f), RayColor.Red),
            (new(0f, 1f, 0f), RayColor.Blue),
            (new(0f, -1f, 0f), RayColor.Green),
            (new(-1f, 0f, 0f), RayColor.Orange),
        };
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(5f, 5f, 5f);
        camera.Target = new Vector3(0f, 0f, 0f);
        camera.Up = new Vector3(0f, 1f, 0f);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        var hblockSize = new Vector3(0.8f, 0.8f, 0.8f);
        var fblockSize = new Vector3(1f, 1f, 1f);
        float plateMove = 0.15f;

        Boolean drawWires = false;
        Boolean drawGrid = true;

        int loopCount = 0;

        while (!Raylib.WindowShouldClose())
        {
            ++loopCount;

            if (loopCount == 144 * 1)
                this.queueAlgo("b'");

            Block[] blocks = cube.blocks;
            int blocksLen = blocks.Length;

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
                Raylib.UpdateCamera(ref camera, CameraMode.ThirdPerson);
            if (Raylib.IsKeyPressed(KeyboardKey.H))
                drawWires = !drawWires;
            if (Raylib.IsKeyPressed(KeyboardKey.G))
                drawGrid = !drawGrid;

            if (_algos.Count != 0)
            {
                int removeAlgoIndex = _algos.Count - 1;
                string alg = _algos[removeAlgoIndex];
                _algos.RemoveAt(removeAlgoIndex);
                cube.doAlgo(alg);
            }

            Raylib.BeginDrawing();
            {
                Raylib.ClearBackground(RayColor.RayWhite);

                Raylib.BeginMode3D(camera);
                {
                    for (int i = 0; i < blocksLen; ++i)
                    {
                        Block block = cube.blocks[i];

                        Vector3 colFacePos =
                            translateCubeToRay(block.primaryVec) * plateMove + blockPos[i];
                        Vector3 colFacePos2 =
                            translateCubeToRay(block.secondaryVec) * plateMove + blockPos[i];

                        if (!drawWires)
                            Raylib.DrawCubeV(blockPos[i], fblockSize, RayColor.Black);
                        else
                            Raylib.DrawCubeWiresV(blockPos[i], fblockSize, RayColor.Black);

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

                        if (cube.blocks[i].GetType() == typeof(Corner))
                        {
                            Vector3 colFacePos3 =
                                translateCubeToRay(((Corner)block).thirdVec) * plateMove
                                + blockPos[i];
                            Raylib.DrawCubeV(
                                colFacePos3,
                                hblockSize,
                                CubeColToRay(block.getFaceColor(((Corner)block).thirdVec))
                            );
                            // var third = new Vec3(blockPos[i].X, 0, 0);
                            // var translate3 = (Vec3)third.Clone();
                            // third.rotateCounterClockwiseY();
                            // third.rotateClockwiseZ();
                            // third.rotateClockwiseZ();
                            // Vector3 colFacePos3 = new(
                            //     translate3.x * plateMove + blockPos[i].X,
                            //     translate3.y * plateMove + blockPos[i].Y,
                            //     translate3.z * plateMove + blockPos[i].Z
                            // );
                            // Raylib.DrawCubeV(
                            //     colFacePos3,
                            //     hblockSize,
                            //     CubeColToRay(block.getFaceColor(third))
                            // );
                        }
                    }
                    foreach (var i in centerPos)
                    {
                        if (!drawWires)
                            Raylib.DrawCubeV(i.Item1, fblockSize, RayColor.Black);
                        Raylib.DrawCubeV(i.Item1 + (i.Item1 * plateMove), hblockSize, i.Item2);
                    }
                    if (drawGrid)
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
