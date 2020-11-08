using Godot;

namespace Moonshot2020.resources.scripts
{
    public class Door : StaticBody2D
    {

        public AnimationPlayer Animation => GetNode<AnimationPlayer>("Animation");
        public CollisionShape2D BoundingBox => GetNode<CollisionShape2D>("BoundingBox");
        public Area2D Area => GetNode<Area2D>("Area");
        public bool IsOpen => BoundingBox.Disabled;


        public override void _Ready()
        {
            Close();
        }

        public void Open()
        {
            PlayAnimation("Open");
        }

        public void Close()
        {
            PlayAnimation("Close");
        }

        public override void _Process(float delta)
        {

        }

        public void PlayAnimation(string animation)
        {
            if (Animation.CurrentAnimation == animation)
                return;
            Animation.Play(animation);
        }

        public void OnAreaBodyEntered(Node body)
        {
            if (body is Player player)
            {
                if (!IsOpen)
                    Open();
            }
        }
        public void OnAreaBodyExited(Node body)
        {
            if (body is Player player)
            {
                if (IsOpen)
                    Close();
            }
        }
    }
}
