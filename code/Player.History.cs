
using System;
using Sandbox;
using Sandbox.UI;

public partial class SledBuildPlayer
{
	private static readonly SoundEvent UndoSound = new( "sounds/balloon_pop_cute.vsnd" )
	{
		Volume = 1,
		DistanceMax = 500.0f
	};
	
	public void AddEntityToHistory( Entity ent )
	{
		SpawnHistory.Push( ent );
	}

	[ServerCmd("undo")]
	public static void UndoLastEntity()
	{
		if ( ConsoleSystem.Caller is not SledBuildPlayer player ) return;

		if ( player.SpawnHistory.Count < 1 ) return;

		player.PlaySound( UndoSound.Name );
		
		Entity ent = player.SpawnHistory.Pop();
		ChatBox.AddInformation( player, "Undone entity", "avatar:" + player.SteamId );
		ent.Delete();
	}
}
