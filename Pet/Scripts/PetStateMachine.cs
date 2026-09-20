using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Pet;

public partial class PetStateMachine : Node2D
{
	[Export]
	public int StateSize { get; set; } = 3;

	private Pet _pet;
	private Array<PetState> _states = new Array<PetState>();

	public PetState CurrentState => _states.Count > 0 ? _states[0] : null;
	public PetState PreviousState => _states.Count > 1 ? _states[1] : null;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Disabled;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PetState newState = CurrentState.Process(delta);
		ChangeState(newState);
	}

	public override void _PhysicsProcess(double delta)
	{
		PetState newState = CurrentState.PhysicsProcess(delta);
		ChangeState(newState);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		PetState newState = CurrentState.HandleInput(@event);
		ChangeState(newState);
	}

	public void ChangeState(PetState newState)
	{
		if (newState == null) return;
		else if (newState == CurrentState) return;

		CurrentState?.Exit();

		_states.Insert(0, newState);

		CurrentState.Enter();

		Error err = _states.Resize(StateSize);

		if (err != Error.Ok)
		{
			GD.PrintErr($"Resize failed: {err}");
		}

		GD.Print($"Change state to {CurrentState}");
	}

	public void HandleInput(InputEvent @event)
	{
		if (CurrentState == null)
		{
			return;
		}

		PetState newState = CurrentState.HandleInput(@event);
		ChangeState(newState);
	}

	public void Init(Pet pet)
	{
		_pet = pet;
		_states.Clear();

		foreach (var child in GetChildren())
		{
			if (child is PetState state)
			{
				_states.Add(state);
			}
		}

		GD.Print($"Init found: {_states.Count} states");

		if (_states.Count == 0) return;

		PetState.Pet = _pet;
		PetState.StateMachine = this;

		foreach (PetState state in _states)
		{
			state.Init();
		}

		// ChangeState(CurrentState.Idle);
		// ChangeState(PreviousState.Walk);

		ProcessMode = ProcessModeEnum.Inherit;
	}
}
