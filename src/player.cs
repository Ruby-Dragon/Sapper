using Godot;
using System;

public partial class player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	private const float AirControl = 0.2f;
	private float Control = 1.0f;
	public float MouseSens = 0.002f;

	private Camera3D TheCamera;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
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
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
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
	}
}
