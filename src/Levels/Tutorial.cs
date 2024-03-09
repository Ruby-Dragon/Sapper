using Godot;
using System;
using Godot.Collections;

public partial class Tutorial : Node3D
{
	// Called when the node enters the scene tree for the first time.
	private Godot.Collections.Array<String> AllTaskMessages = new Array<string>();

	private int CurrentTask = 0;

	[Export] 
	private player ThePlayer;

	[Export] 
	private van Door;
	
	public override void _Ready()
	{
		AllTaskMessages.Add("Detect the mine");
		AllTaskMessages.Add("Place a flag with F");
		AllTaskMessages.Add("Remove a flag with R");
		AllTaskMessages.Add("Flag the mine");
		AllTaskMessages.Add("Go through the door to deploy");
		
		ThePlayer.ChangeMissionText(AllTaskMessages[0]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void NextTask()
	{
		CurrentTask += 1;
		
		ThePlayer.ChangeMissionText(AllTaskMessages[CurrentTask]);

		if (CurrentTask == 4)
		{
			Door.Enabled = true;
		}
	}

	public void Detected()
	{
		if (CurrentTask == 0)
		{
			NextTask();
		}
	}

	public void FlagPlaced()
	{
		if (CurrentTask == 1)
		{
			NextTask();
		}
	}

	public void RemoveFlag()
	{
		if (CurrentTask == 2)
		{
			NextTask();
		}
	}

	public void FlagMine()
	{
		if (CurrentTask == 3)
		{
			NextTask();
		}
	}
}
