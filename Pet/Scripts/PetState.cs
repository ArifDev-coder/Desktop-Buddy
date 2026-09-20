using Godot;
using System;
using System.Diagnostics;

namespace Pet;

public partial class PetState : Node2D
{
	public static Pet Pet { get; set; }
	public static PetStateMachine StateMachine { get; set; }
	public static Vector2I Direction { get; set; }

	public PetState Idle { get; set; }
	public PetState Walk { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Idle = GetNode<PetState>("%Idle");
		Walk = GetNode<PetState>("%Walk");
	}

	public virtual void Init()
	{

	}

	public virtual void Enter()
	{

	}

	public virtual void Exit()
	{

	}

	public virtual PetState HandleInput(InputEvent @event)
	{
		return null;
	}

	public virtual PetState Process(double delta)
	{
		return null;
	}

	public virtual PetState PhysicsProcess(double delta)
	{
		return null;
	}
}
