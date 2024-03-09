using Godot;
using System;

public partial class MainMenu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	private PackedScene FirstLevel;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Confined;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Quit()
	{
		GetTree().Quit();
	}

	public void Start()
	{
		GetTree().ChangeSceneToPacked(FirstLevel);
	}
}
