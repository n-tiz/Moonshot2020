using Godot;
using System;
using Moonshot2020.resources.scripts;

public class Player : KinematicBody2D
{
	private HorizontalMovement _inputHorizontalDirection;
	private VerticalMovement _inputVerticalDirection;

	#region Constants

	public float MaxRunSpeed { get; } = 250;
	public float Acceleration { get; } = 50;
	public float Inertia { get; } = 0.25f;

	#endregion


	public AnimationPlayer Animation => GetNode<AnimationPlayer>("PlayerAnimation");
	public Node2D Visual => GetNode<Node2D>("Visual");
	public Sprite Sprite => GetNode<Sprite>("PlayerSprite");
	public CollisionShape2D BoundingBox => GetNode<CollisionShape2D>("BoundingBox");
	public Vector2 Velocity { get; set; }


	public override void _Ready()
	{
	}

	public override void _Process(float delta)
	{
	}

	public override void _PhysicsProcess(float delta)
	{
		bool canMove = CanMove();
		if (canMove)
			HandleMovements();

		Velocity = MoveAndSlide(Velocity);
		if (_inputHorizontalDirection != HorizontalMovement.None || _inputVerticalDirection != VerticalMovement.None)
			PlayAnimation("Run");
		else
			PlayAnimation("Idle");
	}


	#region Move

	private bool CanMove()
	{
		return true;
	}

	private void HandleMovements()
	{
		float horizontalVelocity;
		float verticalVelocity;

		float LimitSpeed(float speed)
		{
			return Mathf.Min(Mathf.Max(speed, -MaxRunSpeed), MaxRunSpeed);
		}

		_inputHorizontalDirection = (HorizontalMovement)Convert.ToInt32(Input.IsActionPressed(Inputs.MoveRight)) - Convert.ToInt32(Input.IsActionPressed(Inputs.MoveLeft));
		_inputVerticalDirection = (VerticalMovement)Convert.ToInt32(Input.IsActionPressed(Inputs.MoveDown)) - Convert.ToInt32(Input.IsActionPressed(Inputs.MoveUp));

		if (_inputHorizontalDirection == HorizontalMovement.Left && Velocity.x > -MaxRunSpeed)
		{
			horizontalVelocity = LimitSpeed(Velocity.x - Acceleration);
			SetPlayerDirection(HorizontalMovement.Left);
		}
		else if (_inputHorizontalDirection == HorizontalMovement.Right && Velocity.x < MaxRunSpeed)
		{
			horizontalVelocity = LimitSpeed(Velocity.x + Acceleration);
			SetPlayerDirection(HorizontalMovement.Right);
		}
		else
		{
			horizontalVelocity = Mathf.Abs(Velocity.x) <= 1 ? 0 : Mathf.Lerp(Velocity.x, 0, Inertia);
		}
		
		if (_inputVerticalDirection == VerticalMovement.Up && Velocity.y > -MaxRunSpeed)
		{
			verticalVelocity = LimitSpeed(Velocity.y - Acceleration);
		}
		else if (_inputVerticalDirection == VerticalMovement.Down && Velocity.y < MaxRunSpeed)
		{
			verticalVelocity = LimitSpeed(Velocity.y + Acceleration);
		}
		else
		{
			verticalVelocity = Mathf.Abs(Velocity.y) <= 1 ? 0 : Mathf.Lerp(Velocity.y, 0, Inertia);
		}
		Velocity = new Vector2(horizontalVelocity, verticalVelocity);
	}

	#endregion

	public void PlayAnimation(string animation)
	{
		if (Animation.CurrentAnimation == animation)
			return;
		Animation.Play(animation);
	}

	private HorizontalMovement _currentDirection = HorizontalMovement.None;
	public void SetPlayerDirection(HorizontalMovement direction)
	{
		if (direction != HorizontalMovement.None && direction != _currentDirection)
		{
			_currentDirection = direction;
            Visual.Scale = new Vector2((int)direction, 1);
		}
	}
}
