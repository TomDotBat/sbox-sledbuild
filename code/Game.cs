
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

[Library( "sledbuild", Title = "SledBuild" )]
partial class SledBuildGame : Game
{
	public SledBuildGame()
	{
		if ( IsServer )
		{
			// Create the HUD
			new SledBuildHud();
		}
	}

	public override Player CreatePlayer()
	{
		return new SledBuildPlayer();
	}

	[ServerCmd( "spawn" )]
	public static void Spawn( string modelname )
	{
		var owner = ConsoleSystem.Caller;

		if ( ConsoleSystem.Caller == null )
			return;

		var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500 )
			.UseHitboxes()
			.Ignore( owner )
			.Size( 2 )
			.Run();

		var ent = new Prop();
		ent.WorldPos = tr.EndPos;
		ent.WorldRot = Rotation.From( new Angles( 0, owner.EyeAng.yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 );
		ent.SetModel( modelname );
		ent.Owner = owner;
		((SledBuildPlayer)owner).AddEntityToHistory( ent );

		// Drop to floor
		if ( ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1 )
		{
			var p = ent.PhysicsBody.FindClosestPoint( tr.EndPos );

			var delta = p - tr.EndPos;
			ent.PhysicsBody.Pos -= delta;
			//DebugOverlay.Line( p, tr.EndPos, 10, false );
		}

	}

	[ServerCmd( "spawn_entity" )]
	public static void SpawnEntity( string entName )
	{
		var owner = ConsoleSystem.Caller;

		if ( owner == null )
			return;

		var attribute = Library.GetAttribute( entName );

		if ( attribute == null || !attribute.Spawnable )
			return;

		var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 200 )
			.UseHitboxes()
			.Ignore( owner )
			.Size( 2 )
			.Run();

		var ent = Library.Create<Entity>( entName );
		if ( ent is BaseCarriable && owner.Inventory != null )
		{
			if ( owner.Inventory.Add( ent, true ) )
				return;
		}

		ent.WorldPos = tr.EndPos;
		ent.WorldRot = Rotation.From( new Angles( 0, owner.EyeAng.yaw, 0 ) );
		ent.Owner = owner;
		((SledBuildPlayer)owner).AddEntityToHistory( ent );

		//Log.Info( $"ent: {ent}" );
	}
}
