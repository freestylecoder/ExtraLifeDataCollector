open System
open System.IO
open System.Threading

open ExtraLife.Settings
open ExtraLife.Participant
open ExtraLife.Donations
open ExtraLife.Team
open ExtraLife.TeamParticipants

[<EntryPoint>]
let main argv = 
    Directory.CreateDirectory( String.Concat( Settings.OutputDir, "/Data/Participant/" ) ) |> ignore
    Directory.CreateDirectory( String.Concat( Settings.OutputDir, "/Data/ParticipantDonations/" ) ) |> ignore
    Directory.CreateDirectory( String.Concat( Settings.OutputDir, "/Data/Team/" ) ) |> ignore
    Directory.CreateDirectory( String.Concat( Settings.OutputDir, "/Data/TeamParticipants/" ) ) |> ignore

    let participantTimer = new Timer( fun s -> refreshParticipantData() )
    participantTimer.Change( 0, Settings.UpdateTimeInSeconds * 1000 ) |> ignore

    let participantDonationsTimer = new Timer( fun s -> refreshParticipantDonationData() )
    participantDonationsTimer.Change( 0, Settings.UpdateTimeInSeconds * 1000 ) |> ignore

    let teamTimer = new Timer( fun s -> refreshTeamData() )
    teamTimer.Change( 0, Settings.UpdateTimeInSeconds * 1000 ) |> ignore

    let teamParticipantsTimer = new Timer( fun s -> refreshTeamParticipantsData() )
    teamParticipantsTimer.Change( 0, Settings.UpdateTimeInSeconds * 1000 ) |> ignore

    printfn "Extra Life Data Collector"
    printfn "Data output to %s" Settings.OutputDir
    printfn "Press any key to exit..."
    Console.ReadKey() |> ignore

    participantTimer.Dispose()
    participantDonationsTimer.Dispose()
    teamTimer.Dispose()
    teamParticipantsTimer.Dispose()

    0 // return an integer exit code