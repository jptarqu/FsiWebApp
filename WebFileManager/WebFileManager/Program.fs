open WebFileManager
open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let folderPath = argv.[0]
    let startupPath = argv.[1]

    let fsiHelper = new FsiHelper()
    let fileProcessor = new FileProcessor(folderPath, startupPath, fsiHelper.SendToFsi)
    fileProcessor.DoWatch()
    Console.ReadLine() |> ignore
    0 // return an integer exit code
