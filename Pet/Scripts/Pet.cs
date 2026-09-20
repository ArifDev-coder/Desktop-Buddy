using Godot;
using System;

namespace Pet;

public partial class Pet : Node2D
{
	public Window Window { get; set; }

	public Rect2I UsableRect { get; set; }
	public int TargetY { get; set; }
	public Vector2I Direction { get; set; }

	public PetStateMachine PetStateMachine { get; set; }
	public AnimatedSprite2D Anim { get; set; }
	public Area2D Area { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Window = GetWindow();

		GetViewport().TransparentBg = true;
		Window.Transparent = true;
		Window.Borderless = true;
		Window.AlwaysOnTop = true;
		Window.Unresizable = true;

		UsableRect = DisplayServer.ScreenGetUsableRect();
		TargetY = UsableRect.End.Y - Window.Size.Y;
		Window.Position = new Vector2I(0, TargetY);

		Direction = new Vector2I(1, 0);

		Anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		PetStateMachine = GetNode<PetStateMachine>("PetStateMachine");
		Area = GetNode<Area2D>("Area2D");
		Area.InputEvent += OnAreaInput;

		PetStateMachine.Init(this);
	}

	private void OnAreaInput(Node viewport, InputEvent @event, long shapeIdx)
	{
		// GD.Print($"Area dihover! {@event}");

		// GD.Print($"[{shapeIdx}] {@event.GetType().Name} pressed={((@event as InputEventMouseButton)?.Pressed)}");

		// if (@event is not InputEventMouseButton mb) return;
		// if (mb.ButtonIndex != MouseButton.Left || !mb.Pressed) return;

		// ulong now = Time.GetTicksMsec();
		// if (now - _lasClickMs < 1000) return;
		// _lasClickMs = now;

		PetStateMachine.HandleInput(@event);
	}
}
