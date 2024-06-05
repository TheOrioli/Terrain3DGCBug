using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node3D
{
	[Export(PropertyHint.File)]
	private string terrainScenePath;

	private Node terrain;
	// private Resource storage;

	private readonly List<Node3D> meshes = new();

	public void SpawnTerrain()
	{
		terrain = ResourceLoader.Load<PackedScene>(terrainScenePath, cacheMode: ResourceLoader.CacheMode.Ignore).Instantiate();
		var storage = terrain.Get("storage").As<Resource>();
		AddChild(terrain);

		var cam = GetViewport().GetCamera3D();

		for (int i = 0; i < 25; i++)
		{
			var pos = cam.GlobalPosition + new Vector3(GD.Randf() * 25f, 0f, GD.Randf() * 25f) + -cam.GlobalBasis.Z * (float)GD.RandRange(10f, 25f);
			pos.Y = (float)(storage.Call("get_height", pos).AsDouble() + 1.0);
			var mi = new MeshInstance3D
			{
				Mesh = new SphereMesh { Radius = 1, Height = 2 },
				Position = pos,
			};

			AddChild(mi);
			meshes.Add(mi);
		}
	}

	public void RemoveTerrain()
	{
		foreach (var mi in meshes)
		{
			mi.QueueFree();
		}

		meshes.Clear();

		terrain.QueueFree();
		terrain = null;
		// storage = null;
	}

	public void ForceGC()
	{
		GC.Collect();
	}

}
