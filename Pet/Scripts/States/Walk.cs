using Godot;
using Pet;
using System;

namespace Pet.States;

public partial class Walk : PetState
{
	[Export] public int MoveSpeed { get; set; } = 80;
	[Export] public float Acceleration { get; set; } = 400f;

	private float _currentSpeed { get; set; }
	private float _speedMultiplier { get; set; } = 1f;

	private Vector2 _floatPosition;

	public override void Init()
	{
		_floatPosition = Pet.Window.Position;
	}

	public override void Enter()
	{
		GD.Print("Entering Walk State!");

		_currentSpeed = 0f;
		_speedMultiplier = (float)GD.RandRange(0.9, 1.1);

		Pet.Anim.Play("walk");

	}

	public override void Exit()
	{
		GD.Print("Exiting Walk State!");
	}

	public override PetState HandleInput(InputEvent @event)
	{
		// Di Idle State juga sama
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed())
		{
			return Idle;
		}

		return null;
	}

	public override PetState Process(double delta)
	{
		return null;
	}

	public override PetState PhysicsProcess(double delta)
	{
		_currentSpeed = Mathf.Min(_currentSpeed + Acceleration * (float)delta, MoveSpeed * _speedMultiplier);

		Vector2 dir = new Vector2(Pet.Direction.X, Pet.Direction.Y);
		Vector2 moveVector = dir * _currentSpeed * (float)delta;

		_floatPosition += moveVector;
		Pet.Window.Position = new Vector2I(
			Mathf.RoundToInt(_floatPosition.X),
			Mathf.RoundToInt(_floatPosition.Y)
		);

		Pet.Anim.SpeedScale = Mathf.Clamp(_currentSpeed / MoveSpeed, 0.2f, 1.5f);

		if (Pet.Window.Position.X + Pet.Window.Size.X > Pet.UsableRect.End.X)
		{
			Pet.Direction = new Vector2I(-1, 0);
			Pet.Anim.FlipH = true;
		}
		else if (Pet.Window.Position.X < Pet.UsableRect.Position.X)
		{
			Pet.Direction = new Vector2I(1, 0);
			Pet.Anim.FlipH = false;
		}

		return null;
	}
}
