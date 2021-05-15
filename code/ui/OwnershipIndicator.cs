using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class OwnershipIndicator : Panel
{
	public Label Label { get; set; }

	public OwnershipIndicator()
	{
		Label = Add.Label( "Tom.bat", "entity-owner" );
	}
	
	public override void Tick()
	{
		base.Tick();

		Label.Text = "";
		SetClass( "disabled", true );
		
		Player player = Player.Local;
		if ( !player.IsValid() ) return;
		
		TraceResult traceResult = Trace.Ray( player.EyePos, player.EyePos + ( player.EyeRot.Forward * 10000 ) )
			.Ignore( player )
			.Size( 1 )
			.Run();

		Entity entity = traceResult.Entity;
		if ( entity == null ) return;

		Player entityOwner = traceResult.Entity.Owner;
		if ( entityOwner == null ) return;
		
		SetClass( "disabled", false );
		SetClass( "owned", entityOwner == Player.Local );
		Label.Text = entityOwner.Name;
	}
}
