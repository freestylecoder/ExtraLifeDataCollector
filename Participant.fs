module ExtraLife.Participant

open System
open System.IO
open ExtraLife.Settings
open FSharp.Data

let ParticipantUrl = String.Format( Settings.BaseUrl.ToString(), Settings.ParticipantCommand, Settings.ParticipantType, Settings.ParticipantId )

type Participant = JsonProvider<"""
{
    "displayName":"Sample",
    "totalRaisedAmount":-10.00,
    "fundraisingGoal":-20.00,
    "participantID":12345,
    "createdOn":"2000-01-00T00:00:0-0000",
    "avatarImageURL":"//sample.url",
    "teamID":54321,
    "isTeamCaptain":true
} """, RootName="Participant">

let refreshParticipantData () =
    let outputDir = sprintf "%s%s" Settings.OutputDir "/Data/Participant/"
    let data = Participant.Load( ParticipantUrl )

    File.WriteAllText( ( sprintf "%sDisplayName.txt" outputDir ), data.DisplayName )
    File.WriteAllText( ( sprintf "%sTotalRaisedAmount.txt" outputDir ), data.TotalRaisedAmount.ToString( "C2" ) )
    File.WriteAllText( ( sprintf "%sFundraisingGoal.txt" outputDir ), data.FundraisingGoal.ToString( "C2" ) )
    File.WriteAllText( ( sprintf "%sParticipantId.txt" outputDir ), data.ParticipantId.ToString() )
    File.WriteAllText( ( sprintf "%sCreatedOn.txt" outputDir ), DateTime.Parse( data.CreatedOn ).ToLocalTime().ToString() )
    File.WriteAllText( ( sprintf "%sAvatarImageUrl.txt" outputDir ), data.AvatarImageUrl.ToString() )
    File.WriteAllText( ( sprintf "%sTeamId.txt" outputDir ), data.TeamId.ToString() )
    File.WriteAllText( ( sprintf "%sIsTeamCaptain.txt" outputDir ), data.IsTeamCaptain.ToString() )

    File.WriteAllText( ( sprintf "%sProgress.txt" outputDir ), ( sprintf "%s / %s" ( data.TotalRaisedAmount.ToString( "C2" ) ) ( data.FundraisingGoal.ToString( "C2" ) ) ) )

    ()
