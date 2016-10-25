module ExtraLife.TeamParticipants

open System
open System.IO
open ExtraLife.Settings
open FSharp.Data

let TeamParticipantsUrl = String.Format( Settings.BaseUrl.ToString(), Settings.TeamParticipantsCommand, Settings.TeamType, Settings.TeamId )

type TeamParticipants = JsonProvider<"""
[
    {
        "displayName":"Sample",
        "participantID":12345,
        "createdOn":"2000-01-01T00:00:00-0000",
        "avatarImageURL":"//sample.url",
        "isTeamCaptain":true
    }
] """, RootName="Members">

let rec outputTeamParticipantsData (index:int) (teamParticipantData:TeamParticipants.Member list) =
    match teamParticipantData with
    | [] -> ()
    | head::tail ->
        let outputDir = sprintf "%s%s%i/" Settings.OutputDir "/Data/TeamParticipants/" index
        let writeToFile filename text = File.WriteAllText( ( sprintf "%s%s.txt" outputDir filename ), text )

        Directory.CreateDirectory( outputDir ) |> ignore

        File.WriteAllText( ( sprintf "%sDisplayName.txt" outputDir ), head.DisplayName )
        File.WriteAllText( ( sprintf "%sParticipantId.txt" outputDir ), head.ParticipantId.ToString() )
        File.WriteAllText( ( sprintf "%sCreatedOn.txt" outputDir ), head.CreatedOn.ToLocalTime().ToString() )
        File.WriteAllText( ( sprintf "%sAvatarImageUrl.txt" outputDir ), head.AvatarImageUrl.ToString() )
        File.WriteAllText( ( sprintf "%sIsTeamCaptain.txt" outputDir ), head.IsTeamCaptain.ToString() )

        outputTeamParticipantsData ( index + 1 )  tail

let refreshTeamParticipantsData () =
    TeamParticipants.Load( TeamParticipantsUrl )
    |> Array.toList
    |> outputTeamParticipantsData 0

    ()

