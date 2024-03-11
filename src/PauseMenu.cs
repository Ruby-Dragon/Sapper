/*
Copyright (C) 2024 Ruby-Dragon
This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Godot;
using System;

public partial class PauseMenu : ColorRect
{
	[Export] 
	private Control PauseMenuControls;
	
	[Export] 
	private Control SettingsMenuControls;
	
	[Signal]
	public delegate void UpdateSettingsEventHandler();

	[Export] 
	private CheckButton SDFGIButton;

	[Export] 
	private CheckButton SSILButton;
	
	[Export] 
	private CheckButton SSAOButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SDFGIButton.ButtonPressed = GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable;
		SSILButton.ButtonPressed = GetNode<SharedLevelData>("/root/SharedLevelData").SSILEnable;
		SSAOButton.ButtonPressed = GetNode<SharedLevelData>("/root/SharedLevelData").SSAOEnable;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Pause"))
		{
			if (Visible)
			{
				PauseMenuControls.Visible = true;
				SettingsMenuControls.Visible = false;
				
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

	public void SDFGIToggle(bool Setting)
	{
		GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable = Setting;
		EmitSignal("UpdateSettings");
	}
	
	public void SSILToggle(bool Setting)
	{
		GetNode<SharedLevelData>("/root/SharedLevelData").SSILEnable = Setting;
		EmitSignal("UpdateSettings");
	}
	
	public void SSAOToggle(bool Setting)
	{
		GetNode<SharedLevelData>("/root/SharedLevelData").SSAOEnable = Setting;
		EmitSignal("UpdateSettings");
	}
}
