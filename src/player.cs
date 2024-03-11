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

public partial class player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	private const float AirControl = 0.2f;
	private float Control = 1.0f;
	public float MouseSens = 0.002f;
	public float ControllerSens = 0.05f;

	private Camera3D TheCamera;

	private bool dead = false;

	public bool CanLeave = false;

	[Export]
	private detector MetalDetector;

	[Export] 
	public RichTextLabel InteractLabel;

	[Export] 
	private RichTextLabel MissionLabel;

	[Export] 
	private bool FogEnabled;

	[Export] 
	private MeshInstance3D FogMesh;

	[Export] 
	private AudioStreamPlayer3D FootSteps;
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	[Signal]
	public delegate void FalseFlagEventHandler();
	
	[Signal]
	public delegate void UnFalseFlagEventHandler();
	
	[Signal]
	public delegate void UpdateSettingsEventHandler();
	
	[Signal]
	public delegate void PlaceFlagEventHandler();
	
	[Signal]
	public delegate void DetectEventHandler();
	
	[Signal]
	public delegate void RemoveFlagEventHandler();
	
	[Signal]
	public delegate void FlagMineEventHandler();

	public override void _PhysicsProcess(double delta)
	{
		if (dead)
		{
			GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").DeathScene);
		}
		
		if (Input.GetConnectedJoypads().Count > 0)
		{
			Vector2 LookDir = Input.GetVector("ControllerLookLeft", "ControllerLookRight", "ControllerLookDown",
				"ControllerLookUp");
		
			Rotation = new Vector3(Rotation.X, Rotation.Y - (LookDir.X * ControllerSens), Rotation.Z);
			TheCamera.Rotation = new Vector3(Math.Clamp(TheCamera.Rotation.X + (LookDir.Y * ControllerSens), -1.4f, 1.4f), TheCamera.Rotation.Y, TheCamera.Rotation.Z);
		}
		
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
			Control = AirControl;
		}
		else
		{
			Control = 1.0f;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed * Control;
			velocity.Z = direction.Z * Speed * Control;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;

		if (Velocity.LengthSquared() > 0.0f)
		{
			if (!FootSteps.Playing)
			{
				FootSteps.Playing = true;
			}
		}
		else
		{
			FootSteps.Playing = false;
		}
		
		MoveAndSlide();
		
		if (Input.IsActionJustPressed("Flag"))
		{
			MetalDetector.Flag();
		}

		if (Input.IsActionJustPressed("Interact"))
		{
			if (CanLeave)
			{
				GetParent<Level>().GoToNextLevel();
			}
		}
		
		if (Input.IsActionJustPressed("RemoveFlag"))
		{
			MetalDetector.RemoveFlag();
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);

		if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			Rotation = new Vector3(Rotation.X, Rotation.Y - ((InputEventMouseMotion)@event).Relative.X * MouseSens, Rotation.Z);
			TheCamera.Rotation = new Vector3(Math.Clamp(TheCamera.Rotation.X - ((InputEventMouseMotion)@event).Relative.Y * MouseSens, -1.4f, 1.4f), TheCamera.Rotation.Y, TheCamera.Rotation.Z);
		}
	}

	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;

		TheCamera = GetNode<Camera3D>("Camera");

		if (FogEnabled)
		{
			FogMesh.Visible = true;
		}
		else
		{
			FogMesh.Visible = false;
		}
	}

	public void Die()
	{
		dead = true;
	}

	public void OnFalseFlag()
	{
		EmitSignal("FalseFlag");
	}

	public void OnUnFalseFlag()
	{
		EmitSignal("UnFalseFlag");
	}

	public void ToggleCanLeave()
	{
		GD.Print("EnteredTheZone");
		
		if (CanLeave)
		{
			CanLeave = false;
			InteractLabel.Visible = false;
		}
		else
		{
			CanLeave = true;
			InteractLabel.Visible = true;
		}
	}

	public void ChangeMissionText(String text)
	{
		MissionLabel.Text = text;
	}

	public void EmmitUpdateSettings()
	{
		EmitSignal("UpdateSettings");
	}

	public void OnFlag()
	{
		EmitSignal("PlaceFlag");
	}
	
	public void OnDetect()
	{
		EmitSignal("Detect");
	}
	
	public void OnRemoveFlag()
	{
		EmitSignal("RemoveFlag");
	}
	
	public void OnFlagMine()
	{
		EmitSignal("FlagMine");
	}
}
