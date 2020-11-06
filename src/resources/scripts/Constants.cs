namespace Moonshot2020.resources.scripts
{
	public static class Inputs
	{
		public const string MoveLeft = "PlayerMoveLeft";
		public const string MoveRight = "PlayerMoveRight";
		public const string MoveUp = "PlayerMoveUp";
		public const string MoveDown = "PlayerMoveDown";
	}

	public enum HorizontalMovement
	{
		Left = -1,
		None = 0,
		Right = 1
	}
	public enum VerticalMovement
	{
		Up = -1,
		None = 0,
		Down = 1
	}
}
