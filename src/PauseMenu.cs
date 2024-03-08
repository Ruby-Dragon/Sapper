using Godot;
using System;

public partial class PauseMenu : ColorRect
{
	[Export] 
	private Control PauseMenuControls;
	
	[Export] 
	private Control SettingsMenuControls;
	
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

	public void Settings()
	{
		PauseMenuControls.Visible = false;
		SettingsMenuControls.Visible = true;
	}

	public void ExitSettings()
	{
		PauseMenuControls.Visible = true;
		SettingsMenuControls.Visible = false;
	}

	public void SDFGIToggle()
	{
		GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable =
			!GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable;
	}
}
