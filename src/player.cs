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
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	[Signal]
	public delegate void FalseFlagEventHandler();

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
}
