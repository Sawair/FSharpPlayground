open System
open FSharp.Data

type Books =
    HtmlProvider<"https://en.wikipedia.org/wiki/List_of_Pathfinder_books">

let books =
    [for book in Books.GetSample().Tables.``Pathfinder Roleplaying Game Books``.Rows ->
        book.Title, book.Date, book.Pages]
    
let GetBestiaryPages (title:string, _, pages:int) =
    match title with
        | t when t.Contains("Bestiary") -> pages
        | _ -> 0
        
let GetBestiaryPagesFromBooks books =
    [for book in books -> GetBestiaryPages book] |> List.sum

[<EntryPoint>]
let main argv =
    books |> GetBestiaryPagesFromBooks |> printfn "Pathfinder has %d bestiary pages" 
    0 // return an integer exit code
