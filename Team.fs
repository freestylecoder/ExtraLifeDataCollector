module ExtraLife.Team

open System
open System.IO
open ExtraLife.Settings
open FSharp.Data

let TeamUrl = String.Format( Settings.BaseUrl.ToString(), Settings.TeamCommand, Settings.TeamType, Settings.TeamId )

type Team = JsonProvider<"""
{
    "totalRaisedAmount":-10.00,
    "fundraisingGoal":-20.00,
    "createdOn":"2000-01-01T00:00:00-0000",
    "avatarImageURL":"//sample.url",
    "teamID":54321,
    "name":"Sample Team"}
""", RootName="Team">

let refreshTeamData () =
    let outputDir = sprintf "%s%s" Settings.OutputDir "/Data/Team/"
    let teamData = Team.Load( TeamUrl )

    File.WriteAllText( ( sprintf "%s%s" outputDir "Name.txt" ), teamData.Name )
    File.WriteAllText( ( sprintf "%s%s" outputDir "TotalRaisedAmount.txt" ), teamData.TotalRaisedAmount.ToString( "C2" ) )
    File.WriteAllText( ( sprintf "%s%s" outputDir "FundraisingGoal.txt" ), teamData.FundraisingGoal.ToString( "C2" ) )
    File.WriteAllText( ( sprintf "%s%s" outputDir "TeamId.txt" ), teamData.TeamId.ToString() )
    File.WriteAllText( ( sprintf "%s%s" outputDir "CreatedOn.txt" ), teamData.CreatedOn.ToLocalTime().ToString() )
    File.WriteAllText( ( sprintf "%s%s" outputDir "AvatarImageUrl.txt" ), teamData.AvatarImageUrl.ToString() )

    ()
