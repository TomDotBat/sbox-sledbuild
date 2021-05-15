using Sandbox;
using Sandbox.UI;

[Library]
public partial class SledBuildHud : Hud
{
	public SledBuildHud()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/ui/Hud.scss" );

		RootPanel.AddChild<NameTags>();
		RootPanel.AddChild<CrosshairCanvas>();
		RootPanel.AddChild<ChatBox>();
		RootPanel.AddChild<VoiceList>();
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<InventoryBar>();
		RootPanel.AddChild<OwnershipIndicator>();
		RootPanel.AddChild<SpeedIndicator>();
		RootPanel.AddChild<CurrentTool>();
		RootPanel.AddChild<SpawnMenu>();
	}
}
