using Godot;
using System;

public partial class Level : Node
{
	private Marker2D _marker2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_marker2D = (Marker2D) GetNode("StartPosition");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Quit"))
		{
			GetTree().Quit();
		} else if (Input.IsActionJustPressed("Reset"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	private void OnDeathZoneBodyEntered(CharacterBody2D body)
	{
		body.Velocity = Vector2.Zero;
		body.GlobalPosition = _marker2D.GlobalPosition;
	}
}
