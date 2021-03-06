﻿open System
open System.Text.RegularExpressions


let ParseDice dice =
    let regex = Regex(@"(?'quantity'\d+)d(?'size'\d+)(?'modifer'[+-]\d+)?")
    let rr = regex.Match(dice)
    if(rr.Success) then
        let resoult = rr.Groups
        if(resoult.["modifer"].Success) then
            (int resoult.["quantity"].Value, int resoult.["size"].Value, int resoult.["modifer"].Value)
        else
            (int resoult.["quantity"].Value, int resoult.["size"].Value, 0)
    else
        (0,0,0)

let RollDice size =
    let random = Random(Guid.NewGuid().GetHashCode())
    random.Next(size) + 1

let RollDices (quantity, size, modifer) =
    ([ for i in [0.. quantity-1] -> RollDice size ], modifer)
    
let PrintAllDices (list, modifer) =
    printf "Roll resoults:\t( "
    [for i in list -> printf "%d " i] |> ignore
    printfn ")"
    (list, modifer)

let GetResult (list, modifer) =
    List.sum list + modifer
    
let Roll (dice:string) =
    ParseDice dice |> RollDices |> PrintAllDices |> GetResult |> printfn "Roll resoult: %d"
        
let WhetherToContinue (answer:string) =
    match answer with
        | "Y" -> true
        | "y" -> true
        | _ -> false

[<EntryPoint>]
let main argv =
    let mutable rollAgain = true
    while rollAgain do
        printf "What to roll: "
        Console.ReadLine() |> Roll |> ignore
        printf "Next roll?[y/N]: "
        rollAgain <- Console.ReadLine() |> WhetherToContinue
    0
