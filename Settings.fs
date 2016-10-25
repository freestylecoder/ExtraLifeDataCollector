module ExtraLife.Settings

open System.Configuration
open FSharp.Configuration
open FSharp.Data

type Settings = AppSettings<"app.config">
