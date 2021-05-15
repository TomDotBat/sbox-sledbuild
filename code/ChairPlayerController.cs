using Sandbox;

public partial class ChairPlayerController : BasePlayerController
{

	[Net]
	public ChairEntity _chair { get; set; }
	
	public ChairPlayerController() {}
	
	public ChairPlayerController( ChairEntity chair )
	{
		_chair = chair;
	}
	
	public override void Tick()
	{
		if ( !_chair.IsValid() ) ((SledBuildPlayer)Player).VehicleController = null;
		
		Pos = _chair.Transform.PointToWorld( _chair.GetAttachment( "Sit" ).Pos );
		Velocity = _chair.Velocity;
		
		if (Host.IsClient) return;
	
		if (!Input.Pressed(InputButton.Use)) return;
		_chair.Exit();
	}
}
