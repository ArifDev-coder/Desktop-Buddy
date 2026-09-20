using Godot;
using System;
using System.Numerics;

public partial class Walk : PetState
{
	[Export]
	public int MoveSpeed { get; set; } = 1;


	public override void Init()
	{
	}

	public override void Enter()
	{
		GD.Print("Entering Walk State!");

		Pet.Anim.Play("walk");
	}

	public override void Exit()
	{
		GD.Print("Exiting Walk State!");
	}

	public override PetState HandleInput(InputEvent @event)
	{
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
		Vector2I dir = Pet.Direction;
		Vector2I moveVector = dir * MoveSpeed;

		Pet.Window.Position += moveVector;

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
