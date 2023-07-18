using Godot;
using System;

namespace MartianMike.Scripts;

public partial class Player : CharacterBody2D
{
    [Export] private float gravity;
    [Export] private float maxFallSpeed;
    [Export] private float runSpeed;
    [Export] private float jumpForce;
    private AnimatedSprite2D _animatedSprite;
    private float previousDirection;


    public override void _Ready()
    {
        _animatedSprite = (AnimatedSprite2D) GetNode("AnimatedSprite2D");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        var currentVelocity = Velocity;
        var isOnFloor = IsOnFloor();
        
        if (!isOnFloor)
        {
            currentVelocity.Y += gravity * (float) delta;
            currentVelocity.Y = Mathf.Min(currentVelocity.Y, maxFallSpeed);
        }

        if (isOnFloor && Input.IsActionJustPressed("Jump"))
        {
            currentVelocity.Y = -jumpForce;
        }
        
        var desiredDirection = Input.GetAxis("MoveLeft", "MoveRight");

        if (desiredDirection != 0)
        {
            _animatedSprite.FlipH = desiredDirection < 0;    
        }

        UpdateAnimations(desiredDirection);

        currentVelocity.X = desiredDirection * runSpeed * (float) delta;


        Velocity = currentVelocity;
        MoveAndSlide();
        base._PhysicsProcess(delta);
        GD.Print(Velocity);
    }

    private void UpdateAnimations(float desiredDirection)
    {
        if (IsOnFloor())
        {
            if (desiredDirection != 0)
            {
                _animatedSprite.Play("Run");
            }
            else
            {
                _animatedSprite.Play("Idle");
            }
        }
        else
        {
            if (Velocity.Y < 0)
            {
                _animatedSprite.Play("Jump");
            }
            else
            {
                _animatedSprite.Play("Fall");
            }
        }
    }
}