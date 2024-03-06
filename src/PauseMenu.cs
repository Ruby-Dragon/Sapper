using Godot;
using System;

public partial class PauseMenu : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Pause"))
		{
			if (Visible)
			{
				Visible = false;
				GetTree().Paused = false;
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}
			else
			{
				Visible = true;
				GetTree().Paused = true;
				Input.MouseMode = Input.MouseModeEnum.Confined;
			}
		}
	}

	public void Resume()
	{
		Visible = false;
		GetTree().Paused = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public void Quit()
	{
		GetTree().Quit();
	}
}
