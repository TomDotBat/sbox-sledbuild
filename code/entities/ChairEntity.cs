using Sandbox;

[Library( "ent_chair" , Title = "Chair", Spawnable = true )]
public partial class ChairEntity : Prop, IUse
{
	[Net]
	public SledBuildPlayer Sitter { get; set; }
	
	public bool IsInUse
	{
		get
		{
			return Sitter != null;
		}
	}

	public bool OnUse( Entity user )
	{
		if ( IsInUse ) return false;
		if ( user is not SledBuildPlayer player ) return false;

		Enter(player);
		return false;
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	public void Enter(SledBuildPlayer player)
	{
		player.VehicleController = new ChairPlayerController(this);
		player.EnableAllCollisions = false;
		Sitter = player;
	}
	
	public void Exit()
	{
		if (Sitter == null) return;
		
		Sitter.VehicleController = null;
		Sitter.EnableAllCollisions = true;
		Sitter.WorldPos = WorldPos + Vector3.Up * 20 + Vector3.Right * 200;
		Sitter = null;
	}
	
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/chair.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
	}
	
	protected override void OnDestroy()
	{
		Exit();
		base.OnDestroy();
	}
}
