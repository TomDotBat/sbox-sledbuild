
using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class SpeedIndicator : Panel
{
	public SpeedIndicator()
	{
		_label = Add.Label( "420MPH" );
	}

	public override void Tick()
	{
		base.Tick();

		_label.Text = "";
		SetClass( "disabled", true );
	
		Player localPlayer = Player.Local;
		if ( !localPlayer.IsValid() ) return;
		if ( localPlayer is not SledBuildPlayer player ) return;
		
		if ( player.Chair == null ) return;
		
		SetClass( "disabled", false );
		_label.Text = Math.Round((player.Velocity.Length * 2f) / 160) + " MPH";
	}

	private Label _label;
}
