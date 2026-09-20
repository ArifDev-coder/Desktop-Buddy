using Godot;
using Pet;
using System;

namespace Pet.States;

public partial class Idle : PetState
{
	public override void Init()
	{
	}

	public override void Enter()
	{
		GD.Print("Entering Idle State!");

		Pet.Anim.Play("idle");

	}

	public override void Exit()
	{
		GD.Print("Exiting Idle State!");
	}

	public override PetState HandleInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Right && mouseButton.IsPressed())
		{
			GD.Print("Pressed");

			return Walk;
		}

		return null;
	}

	public override PetState Process(double delta)
	{
		return null;
	}

	public override PetState PhysicsProcess(double delta)
	{
		return null;
	}
}
