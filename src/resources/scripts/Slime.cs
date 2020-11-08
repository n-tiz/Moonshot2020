using Godot;

namespace Moonshot2020.resources.scripts
{
    public class Slime : KinematicBody2D
    {
        public AnimationPlayer Animation => GetNode<AnimationPlayer>("SlimeAnimation");
        public CollisionShape2D BoundingBox => GetNode<CollisionShape2D>("BoundingBox");

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        //  // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            PlayAnimation("Attack");
        }

        public void PlayAnimation(string animation)
        {
            if (Animation.CurrentAnimation == animation)
                return;
            Animation.Play(animation);
        }
    }
}
