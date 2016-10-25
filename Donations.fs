module ExtraLife.Donations

open System
open System.IO
open ExtraLife.Settings
open FSharp.Data

let ParticipantDonationsUrl = String.Format( Settings.BaseUrl.ToString(), Settings.ParticipantDonationsCommand, Settings.ParticipantType, Settings.ParticipantId )

type ParticipantDonations = JsonProvider<""" 
    [
        {
            "message":"Sample Message",
            "createdOn":"2000-01-01T00:00:00-0000",
            "donorName":"Sample Donor",
            "avatarImageURL":"//sample.url",
            "donationAmount":-10.00
        },
        {
            "createdOn":"2000-01-01T00:00:00-0000"
        }
    ] """, RootName="Donations">

let rec outputParticipantDonationData (index:int) (participantDonationsData:ParticipantDonations.Donation list) =
    match participantDonationsData with 
    | [] -> ()
    | head::tail ->
        let outputDir = sprintf "%s%s%i/" Settings.OutputDir "/Data/ParticipantDonations/" index
        let writeToFile filename text = File.WriteAllText( ( sprintf "%s%s.txt" outputDir filename ), text )

        Directory.CreateDirectory( outputDir ) |> ignore

        match head.DonorName with
        | None -> "An anonymous donor"
        | Some(s) -> s
        |> writeToFile "DonorName"

        match head.DonationAmount with
        | None -> "made a donation"
        | Some(a) -> a.ToString( "C2" )
        |> writeToFile "DonationAmount"

        match head.Message with
        | None -> ""
        | Some(m) -> m
        |> writeToFile "Message"

        match head.AvatarImageUrl with
        | None -> ""
        | Some(u) -> u
        |> writeToFile "AvatarImageUrl"

        writeToFile "CreatedOn" ( head.CreatedOn.ToLocalTime().ToString() )

        outputParticipantDonationData ( index + 1 )  tail

let refreshParticipantDonationData () =
    ParticipantDonations.Load( ParticipantDonationsUrl )
    |> Array.toList
    |> outputParticipantDonationData 0

    ()

