using Godot;
using System;

public partial class detector : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Godot.Collections.Array<MeshInstance3D> Lights;

	public bool Detection = false;

	private float MineDepth = 0.0f;
		
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Detected(Area3D Mine)
	{
		if (!(Mine is anti_personnel_mine))
		{
			return;
		}
		
		Detection = true;

		MineDepth = ((anti_personnel_mine) Mine).Depth;
		
		GD.Print("Found");
		
		SetLights();
	}
	
	public void EndDetected(Area3D Mine)
	{
		if (!(Mine is anti_personnel_mine))
		{
			return;
		}

		MineDepth = 0.0f;
		Detection = false;
		GD.Print("Unfound");
		
		SetLights();
	}

	private void SetLights()
	{
		for (int i = 0; i < Lights.Count; i++)
		{
			if ((i + 1.0) <= MineDepth)
			{
				Lights[i].SetInstanceShaderParameter("Brightness", 1.0f);
			}
			else
			{
				Lights[i].SetInstanceShaderParameter("Brightness", 0.0f);
			}
		}
	}
}
